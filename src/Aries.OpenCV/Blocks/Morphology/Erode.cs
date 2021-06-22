using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Morphology")]
    public class Erode : MatProcess
    {

        [Category("DATAIN")] public InputArray Element { set; get; }
        [Category("ARGUMENT")] public Point Anchor { set; get; }

        [Category("ARGUMENT")] public int Iterations { set; get; } = 1;

        [Category("ARGUMENT")] public MorphTypes MorphType { set; get; } = MorphTypes.Open;

        [Category("ARGUMENT")] public BorderTypes BorderType { set; get; } = BorderTypes.Constant;

        [Category("ARGUMENT")] public Scalar BorderValue { set; get; }

        public override bool CanExecute()
        {
            return MatIn != null && Element != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.Erode(MatIn, MatOut, Element, Anchor, Iterations, BorderType, BorderValue);
        }
    }
}
