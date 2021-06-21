using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Channel")]
    public class Split : GeneralBlock
    {

        [Category("DATAIN")] public Mat InPutMat { set; get; }

        [Category("DATAOUT")] public Mat RMat { set; get; }
        [Category("DATAOUT")] public Mat GMat { set; get; }
        [Category("DATAOUT")] public Mat BMat { set; get; }


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
