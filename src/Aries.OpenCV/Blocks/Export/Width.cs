using System.ComponentModel;
using Aries.OpenCV.GraphModel;

namespace Aries.OpenCV.Blocks.Export
{
    [Category("Export")]
    public class Width : ExportBlock<int>
    {

        public Width()
        {
            Name = "Width";
        }

        public override void Execute()
        {
            ExportResult = InputMat.Width;
        }

    }


}
