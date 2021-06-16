using System.Linq;
using System.Windows.Controls;
using Aries.OpenCV.GraphModel;
using AriesCV.Controls;
using GalaSoft.MvvmLight.Messaging;
using GraphX.Controls;

namespace AriesCV.Views
{
    /// <summary>
    /// Interaction logic for CVMain.xaml
    /// </summary>
    public partial class CVWorkerView
    {
        public CVWorkerView()
        {
            InitializeComponent();
            Messenger.Default.Register<BlockVertex>(this, "AddBlockToken", AddBlockVertex);
        }

        private void AddBlockVertex(BlockVertex blockVertex)
        {
            GraphCvArea?.AddBlock(blockVertex);
        }

        private GraphCVArea GraphCvArea
        {
            get
            {
                var tabItem = GraphCVTabs.ItemContainerGenerator.ContainerFromItem(GraphCVTabs.SelectedItem) as TabItem;
                return tabItem.FindLogicalChildren<GraphCVArea>().FirstOrDefault();
            }
        }

    }
}
