using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Channel")]
    public class Merge : GeneralBlock
    {
        [Category("INPUT")] public Mat RMat { set; get; }
        [Category("INPUT")] public Mat GMat { set; get; }
        [Category("INPUT")] public Mat BMat { set; get; }

        [Category("OUTPUT")] public Mat MergeMat { set; get; }


        public override void Reload()
        {
            MergeMat = null;
            RMat = null;
            GMat = null;
            BMat = null;
            base.Reload();
        }

        public override bool CanExecute()
        {
            var mats = new[] {RMat, GMat, BMat};
            var matsNotNull = mats.Where(a => a != null).ToList();
            if (matsNotNull.Count == 0)
                return false;

            var targetSize = matsNotNull.First().Size();

            return matsNotNull.All(a => a.Size() == targetSize);
        }

        public override void Execute()
        {
            MergeMat = new Mat();
            var mats = new[] {BMat, GMat, RMat};
            var matsNotNull = mats.Where(a => a != null).ToArray();
            Cv2.Merge(matsNotNull, MergeMat);
        }
    }
}
