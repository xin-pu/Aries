namespace Aries.OpenCV.GraphModel
{
    public abstract class GeneralBlock : BlockVertex
    {
        protected GeneralBlock()
        {
            BlockType = BlockType.General;
        }


        public override void Reload()
        {
            Status = BlockStatus.ToRun;
        }

    }
}
