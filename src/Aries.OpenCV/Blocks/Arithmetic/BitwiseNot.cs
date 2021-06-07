using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class BitwiseNot : GeneralBlock
    {
        [Category("IN_MAT")] public Mat InPut { set; get; }

        [Category("OUT_MAT")] public Mat Output { set; get; }

        [Category("IN_MAT")] public Mat Mask { set; get; }

        [Category("Enter")] public bool EnableMask { set; get; }

        public override void Reload()
        {
            InPut = null;
            Mask = null;
            Output = null;
            base.Reload();
        }

        public override bool CanExecute()
        {
            return InPut != null &&
                   (!EnableMask || Mask != null);
        }

        public override void Execute()
        {
            Output = new Mat();
            Cv2.BitwiseNot(InPut, Output, Mask);
        }
    }

}
