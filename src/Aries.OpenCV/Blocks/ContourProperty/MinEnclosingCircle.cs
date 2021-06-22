using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;
using System.Linq;

namespace Aries.OpenCV.Blocks
{


    [Category("ContourProperty")]
    public class MinEnclosingCircle : ContoursExport<CircleSegment[]>
    {
        public override void Reload()
        {
            CosIn = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return CosIn != null && CosIn.Length > 0;
        }

        public override void Execute()
        {
            var cirs = CosIn.Select(con =>
            {
                Point2f point;
                float radius;
                Cv2.MinEnclosingCircle(con, out point, out radius);
                return new CircleSegment(point, radius);
            });

            Result = cirs.ToArray();
        }


    }
}
