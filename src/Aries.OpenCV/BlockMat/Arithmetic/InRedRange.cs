using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Arithmetic")]
    public class InRedRange : MatProcess
    {
        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            var mat1 = new Mat();
            Cv2.InRange(MatIn, new Scalar(0, 43, 45), new Scalar(10, 255, 255), mat1);


            var mat2 = new Mat();
            Cv2.InRange(MatIn, new Scalar(156, 43, 45), new Scalar(180, 255, 255), mat2);


            MatOut = new Mat();
            MatOut = mat1.Add(mat2);
        }
    }
}
