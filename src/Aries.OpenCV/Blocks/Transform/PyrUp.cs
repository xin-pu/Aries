using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Transform")]
    public class PyrUp : MatProcessingBlock
    {

        [Category("INPUT")] public Size Size { set; get; }

        [Category("Enter")] public BorderTypes BorderType { set; get; } = BorderTypes.Default;

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.PyrUp(InPutMat, OutPutMat, Size, BorderType);
        }
    }
}
