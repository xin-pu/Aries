using System;
using System.ComponentModel;
using Aries.OpenCV.GraphModel;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMat
{
    [Category("Transform")]
    public class SubMatROI : MatProcess
    {

        [Category("DATAIN")] public Rect Rect { set; get; }

        [Category("CHOICE")] public bool EnableSelectRect { set; get; } = true;

        public override bool CanCall()
        {
            return MatIn != null;
        }

        public override void Call()
        {
            if (EnableSelectRect)
            {
                Rect = Cv2.SelectROI(MatIn);
            }

            if (Rect == null)
                throw new ArgumentNullException($"Rect is Null.");
            MatOut = MatIn.SubMat(Rect);
        }
    }
}
