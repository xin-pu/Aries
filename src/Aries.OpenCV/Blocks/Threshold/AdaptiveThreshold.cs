using System;
using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Threshold")]
    public class AdaptiveThreshold : ProcessingBlock
    {

        [Category("Enter")] public double MaxValue { set; get; } = 255;

        [Category("Enter")]
        public AdaptiveThresholdTypes AdaptiveThresholdType { set; get; } = AdaptiveThresholdTypes.MeanC;

        [Category("Enter")] public ThresholdTypes ThresholdType { set; get; } = ThresholdTypes.Binary;

        [Category("Enter")] public int BlockSize { set; get; } = 3;

        [Category("Enter")] public double C { set; get; } = 5;

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
