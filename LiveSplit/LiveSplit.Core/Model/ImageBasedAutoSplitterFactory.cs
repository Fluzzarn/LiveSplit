using LiveSplit.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveSplit.Model;

namespace LiveSplit.Model
{
    public class ImageBasedAutoSplitterFactory
    {
        public static ImageBasedAutoSplitterFactory Instance { get; protected set; }
        static ImageBasedAutoSplitterFactory()
        {
            try
            {
                Instance = new ImageBasedAutoSplitterFactory();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public object Create(LiveSplitState currentState)
        {
            throw new NotImplementedException();
        }
    }
}
