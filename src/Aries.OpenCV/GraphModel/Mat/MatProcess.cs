using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    public abstract class MatProcess : VertexMat
    {
        private Mat _matIn;
        private Mat _matOut;

        [Category("DATAIN")]
        public Mat MatIn
        {
            get => _matIn;
            set
            {
                _matIn = value;
                RaisePropertyChanged(() => MatIn);
            }
        }

        [Category("DATAOUT")]
        public Mat MatOut
        {
            get => _matOut;
            set
            {
                _matOut = value;
                RaisePropertyChanged(() => MatOut);
            }
        }
    }
}