using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("ContourProperty")]
    public class ContourArea : ExportBlock<double>
    {
        [Category("ARGUMENT")] public bool Oriented { set; get; }

        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            Result = Cv2.ContourArea(MatIn, Oriented);
        }
    }
}
