using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Transform")]
    public class DFT : MatProcess
    {

        /// <summary>
        /// The number of channels in the destination image;
        /// if the parameter is 0, the number of the channels will be derived automatically
        /// from src and the code
        /// </summary>
        [Category("ARGUMENT")]
        public DftFlags DftFlag { set; get; } = DftFlags.None;

        /// <summary>
        /// ColorConversionCodes.BGR2HSV
        /// ColorConversionCodes.BGR2GRAY
        /// </summary>
        [Category("ARGUMENT")]
        public int NonzeroRows { set; get; } = 0;

        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();
            Cv2.Dft(MatIn, MatOut, DftFlag, NonzeroRows);
        }
    }
}
