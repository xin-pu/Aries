using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public abstract class ArithmeticBasic : GeneralBlock
    {

        #region INPUT
        [Category("INPUT")] public Mat InPut1 { set; get; }
        [Category("INPUT")] public Mat InPut2 { set; get; }
        [Category("INPUT")] public Mat Mask { set; get; }

        #endregion


        #region OUTPUT
        [Category("OUTPUT")] public Mat Output { set; get; }
        #endregion


        #region Enter Paramter
        [Category("ARGUMENT")] public bool EnableMask { set; get; }
        #endregion


        public override void Reload()
        {
            InPut1 = null;
            InPut2 = null;
            Mask = null;
            Output = null;
            Mask = null;
            base.Reload();

        }

        public override bool CanExecute()
        {
            return InPut1 != null &&
                   InPut2 != null &&
                   InPut1.Size() == InPut2.Size() &&
                   (!EnableMask || Mask != null);
        }

    }
}
