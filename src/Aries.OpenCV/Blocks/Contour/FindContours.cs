using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{
    [Category("Contour")]
    public class FindContours : MatExport<Mat[]>
    {

        [Category("DATAOUT")] public Mat Hierarchy { set; get; }

        [Category("ARGUMENT")] public RetrievalModes RetrievalMode { set; get; } = RetrievalModes.List;

        /// <summary>
        /// CHAIN_APPROX_NONE - translate all the points from the chain code into points;
        /// CHAIN_APPROX_SIMPLE - compress horizontal, vertical, and diagonal segments, that is, the function leaves only their ending points;
        /// CHAIN_APPROX_TC89_L1 - apply one of the flavors of Teh-Chin chain approximation algorithm.
        /// CHAIN_APPROX_TC89_KCOS - apply one of the flavors of Teh-Chin chain approximation algorithm. 
        /// </summary>
        [Category("ARGUMENT")]
        public ContourApproximationModes ContourApproximationMode { set; get; } =
            ContourApproximationModes.ApproxSimple;

        [Category("ARGUMENT")] public Point Offset { set; get; }

        public override void Reload()
        {
            MatIn = null;
            Result = null;
            Status = BlockStatus.ToRun;
        }

        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            Hierarchy = new Mat();
            Mat[] outMats;
            Cv2.FindContours(MatIn, out outMats, Hierarchy, RetrievalMode, ContourApproximationMode, Offset);
            Result = outMats;
        }
    }
}
