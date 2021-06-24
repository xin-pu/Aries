using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockContour
{
    [Category("Contour")]
    public class ApproxPolyDP : ContoursProcess
    {

        [Category("ARGUMENT")] public double Epsilon { set; get; }
        [Category("ARGUMENT")] public bool Closed { set; get; } = true;


        public override bool CanExecute()
        {
            return ConsIn != null && ConsIn.Length > 0;
        }

        public override void Execute()
        {

            ConsOut = ConsIn.Select(con =>
            {
                var outCon = new Mat();
                Cv2.ApproxPolyDP(con, outCon, Epsilon, Closed);
                return outCon;
            }).ToArray();

        }
    }
}
