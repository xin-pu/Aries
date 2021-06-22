using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Transform")]
    public class PyrUp : MatProcess
    {

        [Category("DATAIN")] public Size Size { set; get; }

        [Category("ARGUMENT")] public BorderTypes BorderType { set; get; } = BorderTypes.Default;

        public override void Reload()
        {
            Size = new Size(0, 0);
            base.Reload();
        }

        public override bool CanExecute()
        {
            return MatIn != null && Size != new Size(0, 0);
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.PyrUp(MatIn, MatOut, Size, BorderType);
        }
    }
}
