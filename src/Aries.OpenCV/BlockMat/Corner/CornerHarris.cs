using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Corner")]
    public class CornerHarris : MatProcess
    {

        [Category("ARGUMENT")] public int BlockSize { set; get; } = 2;
        [Category("ARGUMENT")] public int KSize { set; get; } = 3;
        [Category("ARGUMENT")] public double K { set; get; } = 0.04;
        [Category("ARGUMENT")] public BorderTypes BorderType { set; get; } = BorderTypes.Default;


        public override bool CanExecute()
        {
            return MatIn != null;
        }



        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.CornerHarris(MatIn, MatOut, BlockSize, KSize, K, BorderType);
        }



    }
}
