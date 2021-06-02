using System.ComponentModel;
using System.Runtime.InteropServices;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Arithmetic")]
    public class Add : ProcessingBlock
    {
        [Category("IN_MAT")] public Mat InPutMat2 { set; get; }

        public override bool CanExecute()
        {
            var checkRes= InPutMat != null
                   && InPutMat2 != null
                   && InPutMat.Size() == InPutMat2.Size();
            return checkRes;
        }

        public override void Execute()
        {
            OutPutMat = InPutMat.Add(InPutMat2);
        }
    }
}
