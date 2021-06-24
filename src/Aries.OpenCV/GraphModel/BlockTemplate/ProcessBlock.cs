using System.ComponentModel;

namespace Aries.OpenCV.GraphModel
{
    public abstract class ProcessBlock<T1, T2> : VertexMat
    {
        public T1 _tIn;
        public T2 _tOut;

        [Category("DATAIN")]
        public T1 TIn
        {
            get { return _tIn; }
            set
            {
                _tIn = value;
                RaisePropertyChanged(() => TIn);
            }
        }

        [Category("DATAOUT")]
        public T2 TOut
        {
            get { return _tOut; }
            set
            {
                _tOut = value;
                RaisePropertyChanged(() => TOut);
            }
        }

        public override void Reload()
        {
            TIn = default;
            TOut = default;
            Status = BlockStatus.ToRun;
        }


    }
}
