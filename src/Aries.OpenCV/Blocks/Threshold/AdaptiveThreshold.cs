using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Threshold")]
    public class AdaptiveThreshold : MatProcessingBlock
    {

        [Category("ARGUMENT")] public double MaxValue { set; get; } = 255;

        [Category("ARGUMENT")]
        public AdaptiveThresholdTypes AdaptiveThresholdType { set; get; } = AdaptiveThresholdTypes.MeanC;

        [Category("ARGUMENT")] public ThresholdTypes ThresholdType { set; get; } = ThresholdTypes.Binary;

        [Category("ARGUMENT")] public int BlockSize { set; get; } = 3;

        [Category("ARGUMENT")] public double C { set; get; } = 5;

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.AdaptiveThreshold(InPutMat, OutPutMat, MaxValue, AdaptiveThresholdType, ThresholdType, BlockSize, C);
        }
    }
}
