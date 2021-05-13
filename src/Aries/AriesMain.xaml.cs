using System;
using System.Linq;
using Aries.Core;
using Aries.OpenCV.GraphModel;
using GraphX.Common.Enums;
using GraphX.Controls;

namespace Aries
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            AddBlock = Add_Block;
            dg_Area.LogicCore = new LogicCoreCV
            {
                DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.KK,
                DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA,
                DefaultOverlapRemovalAlgorithmParams = {VerticalGap = 50},
                DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None,
                EdgeCurvingEnabled = true
            };
            DataContext = this;
            FileSystemManager.MainWindow = this;
        }

        public ToolKitManager ToolKitManager => ToolKitManager.Instance;
        public FileSystemManager FileSystemManager => FileSystemManager.Instance;
        public AriesManager AriesManager => AriesManager.Instance;


        #region Function about Graph


        #endregion

        public static Action<BlockVertex> AddBlock;

        private void Add_Block(BlockVertex blockVertex)
        {

            dg_Area.AddVertexAndData(blockVertex, new VertexControl(blockVertex));

            //we have to check if there is only one vertex and set coordinates manulay 
            //because layout algorithms skip all logic if there are less than two vertices
            if (dg_Area.VertexList.Count == 1)
            {
                dg_Area.VertexList.First().Value.SetPosition(0, 0);
                dg_Area.UpdateLayout(); //update layout to update vertex size
            }
            else dg_Area.RelayoutGraph(true);

            ZoomControl.ZoomToFill();
        }
    }


}
