using LiveSplit.UI.Components;
using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveSplit.UI;
using AForge.Video.DirectShow;
using System.Drawing;
using AForge.Imaging;

namespace ImageBasedAutoSplit.UI
{
    public abstract class ImageBasedAutoSplitComponent : LogicComponent
    {

        protected ITimerModel Model { get; set; }
        protected VideoCaptureDevice captureDevice;


        private List<Bitmap> ComparisonImages;
        private List<Bitmap> ImagesToDispose;
        private Bitmap currentFrame;


        protected ImageBasedAutoSplitComponent(LiveSplitState state)
        {
            Model = new TimerModel() { CurrentState = state };
            ImagesToDispose = new List<Bitmap>();

            LoadImagesFromLiveSplitState(state);

            captureDevice = new VideoCaptureDevice();
            captureDevice.NewFrame += CaptureDevice_NewFrame;
        }

        private void LoadImagesFromLiveSplitState(LiveSplitState state)
        {
            foreach (var split in state.Run)
            {
                ComparisonImages.Add((Bitmap)System.Drawing.Image.FromFile(split.ImageCompPath));
            }
        }

        private void CaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            if(currentFrame != null)
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
                if(currentFrame != null && ComparisonImages[state.CurrentSplitIndex] != null)
                {
                    Bitmap currentSplitImage = ComparisonImages[state.CurrentSplitIndex];
                    ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);

                    var matchings = tm.ProcessImage(currentFrame, currentSplitImage);

                    if (matchings[0].Similarity >= .95)
                    {
                        Model.Split();
                        ImagesToDispose.Add(currentFrame);
                    }
                }

            }


            if(ImagesToDispose.Any())
            {
                foreach (var img in ImagesToDispose)
                {
                    img.Dispose();

                }

                ImagesToDispose.Clear();
            }
        }
    }
}
