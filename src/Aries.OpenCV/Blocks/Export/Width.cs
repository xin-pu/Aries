using Aries.OpenCV.GraphModel;

namespace Aries.OpenCV.Blocks
{

    public class Width : ExportBlock<int?>
    {
        public override bool CanExecute()
        {
            return InPutMat != null;
        }

        public override void Execute()
        {
            ExportResult = InPutMat?.Width;
        }


    }

}
