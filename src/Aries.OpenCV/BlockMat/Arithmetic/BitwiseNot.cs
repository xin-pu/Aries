using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Arithmetic")]
    public class BitwiseNot : VertexMat
    {
        [Category("DATAIN")] public Mat MatIn { set; get; }
        [Category("DATAIN")] public Mat Mask { set; get; }

        [Category("DATAOUT")] public Mat MatOut { set; get; }

        [Category("CHOICE")] public bool EnableMask { set; get; }


        public override bool CanCall()
        {
            return MatIn != null &&
                   (!EnableMask || Mask != null);
        }

        public override void Call()
        {
            MatOut = new Mat();
            Cv2.BitwiseNot(MatIn, MatOut, Mask);
        }
    }

}
