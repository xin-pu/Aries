using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{

    public abstract class ContoursExport<T> : VertexContour
    {
        private Mat[] _consIn;
        private T _result;

        [Category("DATAIN")]
        public Mat[] ConsIn
        {
            get { return _consIn; }
            set
            {
                _consIn = value;
                RaisePropertyChanged(() => ConsIn);
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
