// Decompiled with JetBrains decompiler
// Type: Aries.OpenCV.Blocks.Import.GetKernel
// Assembly: Aries.OpenCV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 12D39005-470D-4F08-95A3-656A1E60CD00
// Assembly location: E:\Document Code\Code Web\Aries\src\Aries\bin\Debug\Aries.OpenCV.dll

using Aries.OpenCV.GraphModel;
using OpenCvSharp;
using System.ComponentModel;

namespace Aries.OpenCV.BlockMat
{
    [Category("Initial")]
    public class KernelSource : ImportBlock<Mat>
    {
        [Category("ARGUMENT")] public int AnchorPoint_X { set; get; } = -1;

        [Category("ARGUMENT")] public int AnchorPoint_Y { set; get; } = -1;

        [Category("ARGUMENT")] public int ElementWidth { set; get; } = 3;

        [Category("ARGUMENT")] public int ElementHeight { set; get; } = 3;

        [Category("ARGUMENT")] public MorphShapes MorphShapes { set; get; } = MorphShapes.Rect;

        public override bool CanCall()
        {
            return ElementWidth > 0 && ElementHeight > 0;
        }

        public override void Call()
        {
            TOut = Cv2.GetStructuringElement(MorphShapes,
                new Size(ElementWidth, ElementHeight),
                new Point(AnchorPoint_X, AnchorPoint_Y));
        }
    }
}