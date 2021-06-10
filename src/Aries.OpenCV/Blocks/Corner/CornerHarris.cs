using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Corner")]
    public class CornerHarris : MatProcessingBlock
    {

        [Category("ARGUMENT")] public int BlockSize { set; get; } = 2;
        [Category("ARGUMENT")] public int KSize { set; get; } = 3;
        [Category("ARGUMENT")] public double K { set; get; } = 0.04;
        [Category("ARGUMENT")] public BorderTypes BorderType { set; get; } = BorderTypes.Default;


        public override bool CanExecute()
        {
            return InPutMat != null;
        }



        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.CornerHarris(InPutMat, OutPutMat, BlockSize, KSize, K, BorderType);
        }



    }
}
