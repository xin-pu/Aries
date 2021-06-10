using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;

namespace Aries.OpenCV.Blocks
{
    [Category("Contour")]
    public class FitLine : MatProcessingBlock
    {

        /// <summary>
        /// Distance used by the M-estimator
        /// </summary>
        [Category("ARGUMENT")] public DistanceTypes DistType { set; get; } = DistanceTypes.L2;
        /// <summary>
        /// Numerical parameter ( C ) for some types of distances. 
        /// If it is 0, an optimal value is chosen.
        /// </summary>
        [Category("ARGUMENT")] public double Param { set; get; } = 0;

        /// <summary>
        /// Sufficient accuracy for the radius 
        /// (distance between the coordinate origin and the line).
        /// </summary>
        [Category("ARGUMENT")] public double Reps { set; get; } = 0.01;

        /// <summary>
        /// Sufficient accuracy for the angle.
        /// 0.01 would be a good default value for reps and aeps.
        /// </summary>
        [Category("ARGUMENT")] public double Aeps { set; get; } = 0.01;

        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.FitLine(InPutMat, OutPutMat, DistType, Param, Reps, Aeps);
        }


    }
}
