using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockContour.Custom
{
    public class Rotate : MatProcess
    {
        private Mat[] _consIn;

        [Category("DATAIN")]
        public Mat[] ConsIn
        {
            get => _consIn;
            set
            {
                _consIn = value;
                RaisePropertyChanged(() => ConsIn);
            }
        }


        public override bool CanCall()
        {
            return MatIn != null && ConsIn != null;
        }

        public override void Call()
        {
            var i = 1;

            MatOut = MatIn.Clone();
            ConsIn.ToList().ForEach(con =>
            {
                var rotateRec = Cv2.MinAreaRect(con);
                var MinRec = Cv2.BoundingRect(con);
                Cv2.ImWrite($@"D:\O{i}.png", MatIn[MinRec]);

                var angle = rotateRec.Size.Width < rotateRec.Size.Height
                    ? rotateRec.Angle - 90
                    : rotateRec.Angle;

                /// Rotate
                var rotMat = Cv2.GetRotationMatrix2D(rotateRec.Center, angle, 1);
                var outMat = new Mat();
                Cv2.WarpAffine(MatIn, outMat, rotMat, MatIn.Size());
                Cv2.ImWrite($@"D:\R{i}.png", outMat);

                /// Cut
                var centerX = rotateRec.Center.X;
                var centerY = rotateRec.Center.Y;
                var width = rotateRec.Size.Width;
                var height = rotateRec.Size.Height;


                var newCenter = rotateRec.Size.Width < rotateRec.Size.Height
                    ? new Point(centerX - height / 2, centerY - width / 2)
                    : new Point(centerX - width / 2, centerY - height / 2);
                var newW = rotateRec.Size.Width < rotateRec.Size.Height
                    ? new Size(rotateRec.Size.Height, rotateRec.Size.Width)
                    : new Size(rotateRec.Size.Width, rotateRec.Size.Height);
                var rect = new Rect(newCenter, newW);

                outMat = outMat[rect];
                Cv2.ImWrite($@"D:\C{i}.png", outMat);
                i++;

                outMat.CopyTo(MatOut[rect]);
            });
        }
    }
}