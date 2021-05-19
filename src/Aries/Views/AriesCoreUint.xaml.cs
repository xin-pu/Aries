using Aries.Core;
using GraphX.Common.Enums;

namespace Aries.Views
{
    /// <summary>
    /// Interaction logic for AriesCoreUint.xaml
    /// </summary>
    public partial class AriesCoreUint
    {
        /// <summary>
        /// Create For New Command
        /// </summary>
        /// <param name="name"></param>
        public AriesCoreUint(string name)
        {
            InitializeComponent();
            GraphCvCore = new GraphCVCore(name);
            Initial();
            DataContext = this;
        }

        /// <summary>
        /// Create For Open Command
        /// </summary>
        /// <param name="graphCvCore"></param>
        public AriesCoreUint(GraphCVCore graphCvCore)
        {
            InitializeComponent();
            GraphCvCore = graphCvCore;
            Initial();
            DataContext = this;
        }

        public GraphCVCore GraphCvCore { set; get; }

        private void Initial()
        {
            GraphArea.LogicCore = new LogicCoreCV
            {
                DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.KK,
                DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA,
                DefaultOverlapRemovalAlgorithmParams = { VerticalGap = 50 },
                DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None,
                EdgeCurvingEnabled = true
            };

            GraphCvCore.ZoomControl = ZoomControl;
            GraphCvCore.GraphCvArea = GraphArea;
            
        }

    }
}
