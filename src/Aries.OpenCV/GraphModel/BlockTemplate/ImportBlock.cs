using System.ComponentModel;

namespace Aries.OpenCV.GraphModel
{
    public abstract class ImportBlock<T> : BlockVertex
    {
        public T _tout;

        [Category("DATAOUT")]
        public T TOut
        {
            get { return _tout; }
            set
            {
                _tout = value;
                RaisePropertyChanged(() => TOut);
            }
        }

        public override void Reload()
        {
            TOut = default;
            Status = BlockStatus.ToRun;
        }
    }
}
