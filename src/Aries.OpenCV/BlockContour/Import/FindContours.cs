using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockContour
{
    [Category("Import")]
    public class FindContours : VertexContour
    {
        private Mat[] _consOut;
        private Mat _matIn;

        [Category("DATAIN")]
        public Mat MatIn
        {
            get => _matIn;
            set
            {
                _matIn = value;
                RaisePropertyChanged(() => MatIn);
            }
        }

        [Category("DATAOUT")]
        public Mat[] ConsOut
        {
            get => _consOut;
            set
            {
                _consOut = value;
                RaisePropertyChanged(() => ConsOut);
            }
        }

        [Category("DATAOUT")] public Mat Hierarchy { set; get; }

        [Category("ARGUMENT")] public RetrievalModes RetrievalMode { set; get; } = RetrievalModes.List;

        /// <summary>
        ///     CHAIN_APPROX_NONE - translate all the points from the chain code into points;
        ///     CHAIN_APPROX_SIMPLE - compress horizontal, vertical, and diagonal segments, that is, the function leaves only their
        ///     ending points;
        ///     CHAIN_APPROX_TC89_L1 - apply one of the flavors of Teh-Chin chain approximation algorithm.
        ///     CHAIN_APPROX_TC89_KCOS - apply one of the flavors of Teh-Chin chain approximation algorithm.
        /// </summary>
        [Category("ARGUMENT")]
        public ContourApproximationModes ContourApproximationMode { set; get; } =
            ContourApproximationModes.ApproxSimple;

        [Category("ARGUMENT")] public Point Offset { set; get; }


        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            Hierarchy = new Mat();
            Mat[] outMats;
            Cv2.FindContours(MatIn, out outMats, Hierarchy, RetrievalMode, ContourApproximationMode, Offset);
            ConsOut = outMats;
        }
    }
}