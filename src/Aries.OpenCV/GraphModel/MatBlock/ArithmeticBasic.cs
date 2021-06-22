using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    [Category("Arithmetic")]
    public abstract class ArithmeticBasic : BlockVertex
    {

        #region INPUT

        [Category("DATAIN")] public Mat MatIn1 { set; get; }
        [Category("DATAIN")] public Mat MatIn2 { set; get; }
        [Category("DATAIN")] public Mat Mask { set; get; }

        #endregion


        #region OUTPUT

        [Category("DATAOUT")] public Mat MatOut { set; get; }

        #endregion


        #region CHOICE

        [Category("CHOICE")] public bool EnableMask { set; get; }

        #endregion


        public override void Reload()
        {
            MatIn1 = null;
            MatIn2 = null;
            Mask = null;
            MatOut = null;
            Mask = null;
            base.Reload();
        }

        public override bool CanExecute()
        {
            return MatIn1 != null &&
                   MatIn2 != null &&
                   MatIn1.Size() == MatIn2.Size() &&
                   (!EnableMask || Mask != null);
        }

    }
}
