using LiveSplit.Model;
using LiveSplit.TimeFormatters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageBasedAutoSplit
{
    public partial class ImageEditorDialog : Form
    {

        public LiveSplitState CurrentState { get; set; }
        public IRun Run { get; set; }
        public int CurrentSplitIndexOffset { get; set; }
        public bool AllowChangingSegments { get; set; }
        protected BindingList<ISegment> SegmentList { get; set; }
        protected Time PreviousPersonalBestTime;

        protected bool IsInitialized = false;
        public List<Image> ImagesToDispose { get; set; }
        public IList<TimeSpan?> SegmentTimeList { get; private set; }
        public ShortTimeFormatter TimeFormatter { get; private set; }

        protected TimingMethod SelectedMethod
        {
            get { return CurrentState.CurrentTimingMethod; }
            set { CurrentState.CurrentTimingMethod = value; }
        }

        public ImageEditorDialog(LiveSplitState state)
        {
            InitializeComponent();

            CurrentState = state;
            Run = state.Run;
            //Run.PropertyChanged += Run_PropertyChanged;
            PreviousPersonalBestTime = Run.Last().PersonalBestSplitTime;
            //metadataControl.Metadata = Run.Metadata;
            //metadataControl.MetadataChanged += metadataControl_MetadataChanged;
            CurrentSplitIndexOffset = 0;
            AllowChangingSegments = false;
            ImagesToDispose = new List<Image>();
            SegmentTimeList = new List<TimeSpan?>();
            TimeFormatter = new ShortTimeFormatter();
            SegmentList = new BindingList<ISegment>(Run);
            SegmentList.AllowNew = true;
            SegmentList.AllowRemove = true;
            SegmentList.AllowEdit = true;
            SegmentList.ListChanged += SegmentList_ListChanged;
            runGrid.AutoGenerateColumns = false;
            runGrid.AutoSize = true;
            runGrid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            runGrid.DataSource = SegmentList;

            runGrid.CellDoubleClick += runGrid_CellDoubleClick;


            SetupGrid();
            UpdateSegmentList();

        }

        void SegmentList_ListChanged(object sender, ListChangedEventArgs e)
        {
            TimesModified();
        }


        private void TimesModified()
        {
            if (Run.Last().PersonalBestSplitTime.RealTime != PreviousPersonalBestTime.RealTime
                || Run.Last().PersonalBestSplitTime.GameTime != PreviousPersonalBestTime.GameTime)
            {
                Run.Metadata.RunID = null;
                PreviousPersonalBestTime = Run.Last().PersonalBestSplitTime;
            }
           
        }


        private void SetupGrid()
        {

            runGrid.RowHeadersVisible = false;

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Segment Name";
            column.MinimumWidth = 120;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.ReadOnly = true;
            runGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();

            column.DataPropertyName = "ImageCompPath";
            column.Name = "Image Path";
            column.Width = 250;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            runGrid.Columns.Add(column);

        }

        //Refreshes the grid's segments
        private void UpdateSegmentList()
        {
            var previousTime = TimeSpan.Zero;
            SegmentTimeList.Clear();
            foreach (var curSeg in Run)
            {
                if (curSeg == null)
                    SegmentTimeList.Add(null);
                else
                {
                    if (curSeg.PersonalBestSplitTime[SelectedMethod] == null)
                        SegmentTimeList.Add(null);
                    else
                    {
                        SegmentTimeList.Add(curSeg.PersonalBestSplitTime[SelectedMethod] - previousTime);
                        previousTime = curSeg.PersonalBestSplitTime[SelectedMethod].Value;
                    }
                }
            }
        }


        private void runGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Fix()
        {
            Run.FixSplits();
            UpdateSegmentList();
            runGrid.InvalidateColumn(0);
            runGrid.InvalidateColumn(1);

        }

        void runGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0 && e.RowIndex < Run.Count)
            {
                var dialog = new OpenFileDialog();
                dialog.Filter = "Image Files|*.BMP;*.JPG;*.GIF;*.JPEG;*.PNG|All files (*.*)|*.*";
                if (!string.IsNullOrEmpty(Run[e.RowIndex].Name))
                {
                    dialog.Title = "Set Image for " + Run[e.RowIndex].Name + "...";
                }
                else
                {
                    dialog.Title = "Set Image...";
                }
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    try
                    {

                        Run[e.RowIndex].ImageCompPath = dialog.FileName;
                        runGrid.NotifyCurrentCellDirty(true);
                        Fix();
                        //RaiseRunEdited();
                    }
                    catch (Exception ex)
                    {
                        //Log.Error(ex);
                        MessageBox.Show("Could not load image!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
