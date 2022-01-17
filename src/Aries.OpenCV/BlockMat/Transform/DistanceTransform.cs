using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Transform")]
    public class DistanceTransform : MatProcess
    {
        [Category("ARGUMENT")] public DistanceTypes DistanceTypes { set; get; } = DistanceTypes.L2;


        [Category("ARGUMENT")]
        public DistanceTransformMasks DistanceTransformMask { set; get; } = DistanceTransformMasks.Mask5;

        [Category("ARGUMENT")] public int DistType { set; get; } = 4;

        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            MatOut = new Mat();
            Cv2.DistanceTransform(MatIn, MatOut, DistanceTypes, DistanceTransformMask, DistType);
            MatOut.ConvertTo(MatOut, MatType.CV_8U);
        }
    }
}