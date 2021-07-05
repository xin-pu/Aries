using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Threshold")]
    public class AdaptiveThreshold : MatProcess
    {

        [Category("ARGUMENT")] public double MaxValue { set; get; } = 255;

        [Category("ARGUMENT")]
        public AdaptiveThresholdTypes AdaptiveThresholdType { set; get; } = AdaptiveThresholdTypes.MeanC;

        [Category("ARGUMENT")] public ThresholdTypes ThresholdType { set; get; } = ThresholdTypes.Binary;

        [Category("ARGUMENT")] public int BlockSize { set; get; } = 3;

        [Category("ARGUMENT")] public double C { set; get; } = 5;

        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            MatOut = new Mat();
            Cv2.AdaptiveThreshold(MatIn, MatOut, MaxValue, AdaptiveThresholdType, ThresholdType, BlockSize, C);
        }
    }
}
