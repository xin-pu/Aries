using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Channel")]
    public class Split : BlockVertex
    {

        [Category("DATAIN")] public Mat MatIn { set; get; }

        [Category("DATAOUT")] public Mat MatR { set; get; }
        [Category("DATAOUT")] public Mat MatG { set; get; }
        [Category("DATAOUT")] public Mat MatB { set; get; }


        public override void Reload()
        {
            MatIn = null;
            MatR = null;
            MatG = null;
            MatB = null;
            base.Reload();
        }

        public override bool CanExecute()
        {
            return MatIn != null && MatIn.Channels() >= 3;
        }

        public override void Execute()
        {
            var res = MatIn.Split();
            MatB = res[0];
            MatG = res[1];
            MatR = res[2];
        }
    }
}
