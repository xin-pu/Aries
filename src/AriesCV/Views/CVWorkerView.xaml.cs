using Aries.OpenCV.GraphModel;
using AriesCV.Controls;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HandyControl.Controls;

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
            DataContext = this;
            Messenger.Default.Register<string>(this, "AddCVWorkerItemToken", AddCVWorkerItemToken);
            Messenger.Default.Register<BlockVertex>(this, "AddBlockToken", AddBlockVertex);
        }

        private void AddBlockVertex(BlockVertex obj)
        {
            GraphCvAreaAtWorkSpace?.AddBlock(obj);
        }

        private void AddCVWorkerItemToken(string graphName)
        {
            var panel = new CVWorkerItemView
            {
                GraphCVArea =
                {
                    Name = graphName
                }
            };
            var tabItem = new TabItem
            {
                Header = panel.GraphCVArea.Name,
                Content = panel,
            };
            GraphCVTabs.Items.Add(tabItem);
            GraphCVTabs.SelectedItem = tabItem;
        }

        public GraphCVArea GraphCvAreaAtWorkSpace { set; get; }

        public CVWorkerItemView CvWorkerItem { set; get; }


        #region Command

        public RelayCommand SelectWorkUnitCommand
        {
            get { return new RelayCommand(SelectWorkUnitCommand_Execute, SelectWorkUnitCommand_CanExecute); }
        }

        private bool SelectWorkUnitCommand_CanExecute()
        {
            return GraphCVTabs.SelectedContent != null;
        }

        private void SelectWorkUnitCommand_Execute()
        {
            CvWorkerItem = (CVWorkerItemView) GraphCVTabs.SelectedContent;
            GraphCvAreaAtWorkSpace = CvWorkerItem.GraphCVArea;
        }


        #endregion

    }
}
