using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    public abstract class ContoursProcess : BlockVertex
    {
        private Mat[] _consIn;
        private Mat[] _consOut;

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
        public Mat[] ConsOut
        {
            get { return _consOut; }
            set
            {
                _consOut = value;
                RaisePropertyChanged(() => ConsOut);
            }
        }


        public override void Reload()
        {
            ConsIn = null;
            ConsOut = null;
            Status = BlockStatus.ToRun;
        }
    }
}
