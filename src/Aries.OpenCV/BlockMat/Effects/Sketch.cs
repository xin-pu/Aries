using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat.Effects
{
    public class Sketch : MatProcess
    {
        public override bool CanExecute()
        {
            return MatIn != null;
        }

        public override void Execute()
        {
            MatOut = new Mat();

            // 去色
            var gray = MatIn.CvtColor(ColorConversionCodes.BGR2GRAY);
            gray = gray.CvtColor(ColorConversionCodes.GRAY2BGR);

            // 反色
            var grayBitwiseNot = new Mat();
            Cv2.BitwiseNot(gray, grayBitwiseNot);

            // 高斯模糊
            var gausss = grayBitwiseNot.GaussianBlur(new Size(7, 7), 0);


            // 减淡
            gray.CopyTo(MatOut);
            var size = MatOut.Size();
            var d = MatOut.Channels();
            unsafe
            {
                for (var row = 0; row < size.Height; row++)
                {
                    for (var col = 0; col < size.Width; col++)
                    {

                        var grayChan = gray.Ptr(row, col);
                        var grayChanP = (byte*) grayChan.ToPointer();

                        var gaussChan = gausss.Ptr(row, col);
                        var gaussChannP = (byte*) gaussChan.ToPointer();

                        var outChan = MatOut.Ptr(row, col);
                        var outChanP = (byte*) outChan.ToPointer();

                        for (var chan = 0; chan < d; chan++)
                        {
                            var a = grayChanP[chan];
                            var b = gaussChannP[chan];
                            var c = (byte) (new double[] {a + (a * b) / (255 - b), 255}).Min();

                            outChanP[chan] = c;

                        }

                    }
                }
            }
        }
    }
}
