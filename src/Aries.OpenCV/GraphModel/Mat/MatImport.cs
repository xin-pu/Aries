using System.ComponentModel;
using OpenCvSharp;

namespace Aries.OpenCV.GraphModel
{
    public abstract class MatImport : VertexMat
    {
        private Mat _matOut;

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


        public override void Reload()
        {
            MatOut = null;
            Status = BlockStatus.ToRun;
        }
    }
}
