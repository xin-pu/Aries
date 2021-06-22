using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class BitwiseNot : GeneralBlock
    {
        [Category("DATAIN")] public Mat InPut { set; get; } 
        [Category("DATAIN")] public Mat Mask { set; get; }

        [Category("DATAOUT")] public Mat Output { set; get; }

        [Category("CHOICE")] public bool EnableMask { set; get; }

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
