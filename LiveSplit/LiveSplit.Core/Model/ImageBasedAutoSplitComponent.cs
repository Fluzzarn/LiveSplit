using AForge.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Livesplit.Model
{
    public class ImageBasedAutoSplitComponent : LogicComponent
    {

        protected ITimerModel Model { get; set; }
        public static string StreamingDevice;
        public override string ComponentName
        {
            get
            {
                return "Imagebased Autosplitter";
            }
        }

        protected VideoCaptureDevice captureDevice;


        private List<Bitmap> ComparisonImages;
        private List<Bitmap> ImagesToDispose;
        private Bitmap currentFrame;
        private ExhaustiveTemplateMatching tm;

        public ImageBasedAutoSplitComponent(LiveSplitState state)
        {
            Model = new TimerModel() { CurrentState = state };
            ImagesToDispose = new List<Bitmap>();
            ComparisonImages = new List<Bitmap>();
            LoadImagesFromLiveSplitState(state);

            captureDevice = new VideoCaptureDevice(StreamingDevice);


            captureDevice.NewFrame += new NewFrameEventHandler(CaptureDevice_NewFrame);
                captureDevice.Start();
            tm = new ExhaustiveTemplateMatching(0);

        }

        private void LoadImagesFromLiveSplitState(LiveSplitState state)
        {
            foreach (var split in state.Run)
            {
                if (File.Exists(split.ImageCompPath))
                    ComparisonImages.Add((Bitmap)System.Drawing.Image.FromFile(split.ImageCompPath));
            }
        }

        private void CaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            if (currentFrame != null)
            {
                var oldBitmap = currentFrame;
                ImagesToDispose.Add(oldBitmap);
            }

            try
            {
                currentFrame = (Bitmap)eventArgs.Frame.Clone();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            if (state.CurrentPhase == TimerPhase.NotRunning)
            {
                if (currentFrame != null && ComparisonImages[state.CurrentSplitIndex] != null)
                {
                    Bitmap currentSplitImage = ComparisonImages[state.CurrentSplitIndex];
                    ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);

                    var matchings = tm.ProcessImage(currentFrame, currentSplitImage);

                    if (matchings[0].Similarity >= .95)
                    {
                        Model.Start();
                        ImagesToDispose.Add(currentFrame);
                    }
                }
            }
            else if (state.CurrentPhase == TimerPhase.Running)
            {
                if (currentFrame != null && ComparisonImages[state.CurrentSplitIndex] != null)
                {
                    
                    
                    var matchings = tm.ProcessImage(currentFrame, ComparisonImages[state.CurrentSplitIndex]);
                    
                    if (matchings[0].Similarity * 100 >= 100 - state.CurrentSplit.ImageSimilarityThreshold)
                    {
                        Model.Split();
                        ImagesToDispose.Add(currentFrame);
                    }
                }

            }


            //if (ImagesToDispose.Any())
            //{
            //    foreach (var img in ImagesToDispose)
            //    {
            //        img.Dispose();
            //
            //    }
            //
            //    ImagesToDispose.Clear();
            //}
        }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            throw new NotImplementedException();
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            throw new NotImplementedException();
        }

        public override void SetSettings(XmlNode settings)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            foreach (var img in ComparisonImages)
            {
                img.Dispose();

            }

            foreach (var img in ImagesToDispose)
            {
                img.Dispose();

            }


            if (currentFrame != null)
            {
                currentFrame.Dispose();
            }

            captureDevice.SignalToStop();
            captureDevice.WaitForStop();
        }
    }
}