using Aries.OpenCV.GraphModel;

namespace Aries.OpenCV.Blocks.Export
{
    public class Width : ExportBlock<int>
    {
        public override void Execute()
        {
            ExportResult = InputMat.Width;
        }

    }

    
}
