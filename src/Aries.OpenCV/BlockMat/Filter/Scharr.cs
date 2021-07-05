using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Filter")]
    public class Scharr : MatProcess
    {
        [Category("ARGUMENT")] public MatType MatType { set; get; }

        [Category("ARGUMENT")] public int XOrder { set; get; } = 0;

        [Category("ARGUMENT")] public int YOrder { set; get; } = 0;

        [Category("ARGUMENT")] public int KSize { set; get; } = 3;

        [Category("ARGUMENT")] public double Scale { set; get; } = 1;

        [Category("ARGUMENT")] public double Delta { set; get; } = 0;

        [Category("ARGUMENT")] public BorderTypes BorderType { set; get; } = BorderTypes.Default;

        public override bool CanCall()
        {
            var resCheck = XOrder <= 2 && XOrder >= 0;
            resCheck = resCheck && YOrder <= 2 && YOrder >= 0;
            return resCheck && MatIn != null;
        }

        public override void Call()
        {
            MatOut = new Mat();
            Cv2.Scharr(MatIn, MatOut, MatType, XOrder, YOrder, Scale, Delta, BorderType);
        }
    }
}
