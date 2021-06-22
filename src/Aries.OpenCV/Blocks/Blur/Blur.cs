using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.Blocks
{

    [Category("Blur")]
    public class Blur : MatProcess
    {
        [Category("ARGUMENT")] public Size KSize { set; get; } = new Size(3, 3);


        [Category("ARGUMENT")] public Point AnchorPoint { set; get; } = new Point(-1, -1);

        /// <summary>
        /// The border mode used to extrapolate pixels outside of the image
        /// </summary>
        [Category("ARGUMENT")]
        public BorderTypes BorderTypes { set; get; } = BorderTypes.Default;


        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.Blur(MatIn, MatOut, KSize, AnchorPoint, BorderTypes);
        }


    }

}
