using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockContour
{
    [Category("Logic")]
    public abstract class ChoiceContoursForCustom : ContoursProcess
    {
        public abstract Mat[] Filter();


        public override bool CanCall()
        {
            return ConsIn != null && ConsIn.Length > 0;
        }

        public override void Call()
        {
            ConsOut = new Mat[0];
            ConsOut = Filter();
        }
    }
}
