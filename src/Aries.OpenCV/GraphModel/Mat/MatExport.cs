using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{

    public abstract class MatExport<T> : VertexMat
    {
        private Mat _matIn;
        public T _result;

        [Category("DATAIN")]
        public Mat MatIn
        {
            get { return _matIn; }
            set
            {
                _matIn = value;
                RaisePropertyChanged(() => MatIn);
            }
        }

        [Category("DATAOUT")]
        public T Result
        {
            get { return _result; }
            set
            {
                _result = value;
                RaisePropertyChanged(() => Result);
            }
        }


    }
}
