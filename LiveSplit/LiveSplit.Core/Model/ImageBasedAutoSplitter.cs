using LiveSplit.Model;
using LiveSplit.Options;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Livesplit.Model
{
    public class ImageBasedAutoSplitter : ICloneable
    {
        public bool IsActivated => Component != null;
        public IComponent Component { get; set; }
        public IComponentFactory Factory { get; set; }

        public void Activate(LiveSplitState state)
        {
            if (!IsActivated)
            {
                try
                {
                    foreach (var split in state.Run)
                    {
                        if (split.ImageCompPath != null)
                        {
                            Component = Factory.Create(state);
                            break;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    MessageBox.Show(state.Form, "The Auto Splitter could not be activated. (" + ex.Message + ")", "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public object Clone()
        {
            return new AutoSplitter()
            {
                Component = Component,
                Factory = Factory
            };
        }
    }
}
