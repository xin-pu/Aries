using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;
using System.Linq;

namespace Aries.OpenCV.BlockContour
{
    [Category("Property")]
    public class FitLine : ContoursExport<Mat[]>
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

        public override bool CanCall()
        {
            return ConsIn != null;
        }

        public override void Call()
        {

            Result = ConsIn.Select(a =>
            {
                Mat res = new Mat();
                Cv2.FitLine(a, res, DistType, Param, Reps, Aeps);
                return res;
            }).ToArray();

        }


    }
}
