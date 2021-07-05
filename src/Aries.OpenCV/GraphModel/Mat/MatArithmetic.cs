using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    [Category("Arithmetic")]
    public abstract class MatArithmetic : VertexMat
    {
        private Mat _matIn1;
        private Mat _matIn2;
        private Mat _mask;
        private Mat _matOut;

        #region INPUT

        [Category("DATAIN")]
        public Mat MatIn1
        {
            get { return _matIn1; }
            set
            {
                _matIn1 = value;
                RaisePropertyChanged(() => MatIn1);
            }
        }

        [Category("DATAIN")]
        public Mat MatIn2
        {
            get { return _matIn2; }
            set
            {
                _matIn2 = value;
                RaisePropertyChanged(() => MatIn2);
            }
        }

        [Category("DATAIN")]
        public Mat Mask
        {
            get { return _mask; }
            set
            {
                _mask = value;
                RaisePropertyChanged(() => Mask);
            }
        }

        #endregion


        #region OUTPUT

        [Category("DATAOUT")]
        public Mat MatOut
        {
            get { return _matOut; }
            set
            {
                _matOut = value;
                RaisePropertyChanged(() => MatOut);
            }
        }

        #endregion


        #region CHOICE

        [Category("CHOICE")] public bool EnableMask { set; get; }

        #endregion




        public override bool CanCall()
        {
            return MatIn1 != null &&
                   MatIn2 != null &&
                   MatIn1.Size() == MatIn2.Size() &&
                   (!EnableMask || Mask != null);
        }

    }
}
