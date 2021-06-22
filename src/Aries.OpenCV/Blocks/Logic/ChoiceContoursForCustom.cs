using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Logic")]
    public abstract class ChoiceContoursForCustom : ContoursProcess
    {
        public abstract Mat[] Filter();

        public override void Reload()
        {
            ConsIn = null;
            ConsOut = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return ConsIn != null && ConsIn.Length > 0;
        }

        public override void Execute()
        {
            ConsOut = new Mat[0];
            ConsOut = Filter();
        }
    }
}
