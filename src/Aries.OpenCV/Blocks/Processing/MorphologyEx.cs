// Decompiled with JetBrains decompiler
// Type: Aries.OpenCV.Blocks.Processing.MorphologyEx
// Assembly: Aries.OpenCV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 12D39005-470D-4F08-95A3-656A1E60CD00
// Assembly location: E:\Document Code\Code Web\Aries\src\Aries\bin\Debug\Aries.OpenCV.dll

using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;

namespace Aries.OpenCV.Blocks.Processing
{
    [Category("Morphology")]
    public class MorphologyEx : ProcessingBlock
    {
        [Category("IN_MAT")]
        public InputArray Shape { set; get; }

        [Category("Enter")]
        public MorphTypes MorphType { set; get; } = MorphTypes.Open;

        public override bool CanExecute()
        {
            return InPutMat != null && Shape != null;
        }

        public override void Execute()
        {
            OutPutMat = new Mat();
            Cv2.MorphologyEx(InPutMat, OutPutMat, MorphType, Shape, new Point?(), 1, BorderTypes.Constant, new Scalar?());
        }
    }
}