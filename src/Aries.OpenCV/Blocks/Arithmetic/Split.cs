using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class Split : GeneralBlock
    {

        [Category("IN_MAT")] public Mat InPutMat { set; get; }

        [Category("OUT_MAT")] public Mat RMat { set; get; }
        [Category("OUT_MAT")] public Mat GMat { set; get; }
        [Category("OUT_MAT")] public Mat BMat { set; get; }


        public override void Reload()
        {
            InPutMat = null;
            RMat = null;
            GMat = null;
            BMat = null;
            base.Reload();
        }

        public override bool CanExecute()
        {
            return InPutMat != null && InPutMat.Channels() >= 3;
        }

        public override void Execute()
        {
            var res = InPutMat.Split();
            BMat = res[0];
            GMat = res[1];
            RMat = res[2];
            
        }
    }
}
