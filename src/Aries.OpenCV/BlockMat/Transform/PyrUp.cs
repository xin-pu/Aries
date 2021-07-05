using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Transform")]
    public class PyrUp : MatProcess
    {

        [Category("DATAIN")] public Size Size { set; get; }

        [Category("ARGUMENT")] public BorderTypes BorderType { set; get; } = BorderTypes.Default;



        public override bool CanCall()
        {
            return MatIn != null && Size != new Size(0, 0);
        }

        public override void Call()
        {
            MatOut = new Mat();
            Cv2.PyrUp(MatIn, MatOut, Size, BorderType);
        }
    }
}
