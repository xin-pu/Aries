namespace Aries.OpenCV.GraphModel
{
    public abstract class GeneralBlock : BlockVertex
    {


        public override void Reload()
        {
            Status = BlockStatus.ToRun;
        }

    }
}
