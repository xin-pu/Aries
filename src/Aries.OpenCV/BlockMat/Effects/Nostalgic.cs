using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat.Effects
{
    /// <summary>
    /// R=0.393r+0.769g+0.189b
    /// G=0.349r+0.686g+0.168b
    /// B=0.272r+0.534g+0.131b
    /// </summary>
    public class Nostalgic : MatProcess
    {
        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            MatOut = new Mat();


            MatIn.CopyTo(MatOut);
            var size = MatOut.Size();
            unsafe
            {
                for (var row = 0; row < size.Height; row++)
                {
                    for (var col = 0; col < size.Width; col++)
                    {

                        var matInChan = MatIn.Ptr(row, col);
                        var matInChanP = (byte*) matInChan.ToPointer();


                        var outChan = MatOut.Ptr(row, col);
                        var outChanP = (byte*) outChan.ToPointer();


                        var b = matInChanP[0];
                        var g = matInChanP[1];
                        var r = matInChanP[2];

                        outChanP[0] = (byte) (0.272 * r + 0.534 * g + 0.131 * b);
                        outChanP[1] = (byte) (0.349 * r + 0.686 * g + 0.168 * b);
                        outChanP[2] = (byte) (0.393 * r + 0.769 * g + 0.189 * b);


                    }
                }
            }
        }
    }
}
