using HandyControl.Controls;

namespace AriesCV.Controls
{


    public class CvPropertyGrid : PropertyGrid
    {

        public override PropertyResolver PropertyResolver { get; } = new CVPropertyResolver();


    }
}
