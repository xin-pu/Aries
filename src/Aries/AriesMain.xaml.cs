using System.Linq;
using Aries.Core;
using Aries.OpenCV.Blocks.Processing;
using Aries.OpenCV.GraphModel;
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
            DataContext = this;
        }

        public ToolKitManager ToolKitManager => ToolKitManager.Instance;
        public FileSystemManager FileSystemManager => FileSystemManager.Instance;
        public AriesManager AriesManager => AriesManager.Instance;


        #region Function about Graph

        private void OnTestClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var data = fillDataVertex(new Blur());
            dg_Area.AddVertexAndData(data, new VertexControl(data));

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

        private BlockVertex fillDataVertex(BlockVertex item)
        {
            item.Name = "123";
            return item;
        }

        #endregion

    }


}
