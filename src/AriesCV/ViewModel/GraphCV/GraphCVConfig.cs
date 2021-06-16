using Aries.OpenCV.Core;

namespace AriesCV.ViewModel
{
    public class GraphCVConfig
    {
        public LayoutType LayoutAlgorithm { set; get; }
            = LayoutType.TreeTopTpBottom;

        public EdgeRoutingType EdgeRoutingAlgorithm { set; get; } 
            = EdgeRoutingType.SimpleER;
    }
}