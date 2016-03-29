using LiveSplit.UI.Components;
using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveSplit.UI;

namespace ImageBasedAutoSplit.UI
{
    public abstract class ImageBasedAutoSplitComponent : LogicComponent
    {

        protected ITimerModel Model { get; set; }

        protected ImageBasedAutoSplitComponent(LiveSplitState state)
        {
            Model = new TimerModel() { CurrentState = state };
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            if (state.CurrentPhase == TimerPhase.NotRunning)
            {

            }
            else if (state.CurrentPhase == TimerPhase.Running)
            {
            }
        }
    }
}
