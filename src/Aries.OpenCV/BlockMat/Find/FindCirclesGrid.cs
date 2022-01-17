using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Filter")]
    public class FindCirclesGrid : MatProcess
    {
        [Category("ARGUMENT")] public float ThresholdStep { set; get; } = 10;
        [Category("ARGUMENT")] public float MinThreshold { set; get; } = 50;
        [Category("ARGUMENT")] public float MaxThreshold { set; get; } = 200;


        [Category("ARGUMENT")] public bool FilterByArea { set; get; } = true;
        [Category("ARGUMENT")] public float MinArea { set; get; } = 10000;
        [Category("ARGUMENT")] public float MaxArea { set; get; } = 400000;


        [Category("ARGUMENT")] public bool FilterByCircularity { set; get; } = true;
        [Category("ARGUMENT")] public float MinCircularity { set; get; } = 0.7f;
        [Category("ARGUMENT")] public float MaxCircularity { set; get; } = float.MaxValue;


        [Category("ARGUMENT")] public bool FilterByConvexity { set; get; } = true;
        [Category("ARGUMENT")] public float MinConvexity { set; get; } = 0.95f;
        [Category("ARGUMENT")] public float MaxConvexity { set; get; } = float.MaxValue;


        [Category("ARGUMENT")] public Size PatternSize { set; get; } = new(5, 5);

        [Category("ARGUMENT")] public byte BlobColor { set; get; }
        [Category("DATAOUT")] public int CornerCount { set; get; }

        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            var paras = new SimpleBlobDetector.Params
            {
                FilterByColor = false,

                ThresholdStep = ThresholdStep,
                /// 阈值控制
                MinThreshold = MinThreshold,
                MaxThreshold = MaxThreshold,

                /// 像素面积控制
                FilterByArea = FilterByArea,
                MinArea = MinArea,
                MaxArea = MaxArea,

                /// 圆
                FilterByCircularity = FilterByCircularity,
                MinCircularity = MinCircularity,
                MaxCircularity = MaxCircularity,

                /// 凸
                FilterByConvexity = FilterByConvexity,
                MinConvexity = MinConvexity,
                MaxConvexity = MaxConvexity,

                BlobColor = BlobColor
            };
            var simpleBlob = SimpleBlobDetector.Create(paras);
            var corners = new Mat<Point2f>();
            Cv2.FindCirclesGrid(MatIn, PatternSize, corners,
                FindCirclesGridFlags.AsymmetricGrid | FindCirclesGridFlags.Clustering, simpleBlob);
            CornerCount = corners.ToArray().Length;
        }
    }
}