using LiveSplit.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageBasedAutoSplit.UI
{
    public class ImageBasedAutoSplitterFactory
    {
        public static ImageBasedAutoSplitterFactory Instance { get; protected set; }
        static ImageBasedAutoSplitterFactory()
        {
            try
            {
                Instance = new ImageBasedAutoSplitterFactory();
                Instance.Init();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

    }
}
