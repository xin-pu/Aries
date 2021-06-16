using Aries.OpenCV.GraphModel;
using AriesCV.Controls;
using AriesCV.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using HandyControl.Controls;

namespace AriesCV.Views
{
    /// <summary>
    /// Interaction logic for CVMain.xaml
    /// </summary>
    public partial class CVWorkerContainerView
    {
        public CVWorkerContainerView()
        {
            InitializeComponent();
            RegisterMessenger();
        }

        #region  属性

        public GraphCVArea GraphCvAreaAtWorkSpace { set; get; }
        public CVWorkerItemView CvWorkerItem { set; get; }

        #endregion





        private void RegisterMessenger()
        {
            Messenger.Default.Register<BlockVertex>(this, "AddBlockToken", AddBlockVertex);

            Messenger.Default.Register<TabItem>(this, "AddNewTabToken", AddNewTab);
            Messenger.Default.Register<string>(this, "RemoveTabToken", RemoveTab);
            Messenger.Default.Register<string>(this, "ClearTabToken", ClearNewTab);
            
        }

        private void AddBlockVertex(BlockVertex obj)
        {
            GraphCvAreaAtWorkSpace?.AddBlock(obj);
        }

        private void AddNewTab(TabItem tabItem)
        {
            GraphCVTabs.Items.Add(tabItem);
            GraphCVTabs.SelectedItem = tabItem;
        }

        private void RemoveTab(string obj)
        {
            var tabItem = GraphCVTabs.SelectedItem;
            GraphCVTabs.Items.Remove(tabItem);
        }
        private void ClearNewTab(string obj)
        {
            GraphCVTabs.Items.Clear();
        }



        #region Command

        private void GraphCVTabs_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var cvWorkerItem = CvWorkerItem = (CVWorkerItemView) GraphCVTabs.SelectedContent;
            if (cvWorkerItem == null) return;
            var graphCvAreaAtWorkSpace = GraphCvAreaAtWorkSpace = CvWorkerItem.GraphCVArea;
            var model = ViewModelLocator.Instance.CVWorkerModel;
            model.GraphCvAreaWorking = graphCvAreaAtWorkSpace;
            model.CvWorkerItem = cvWorkerItem;
        }


        #endregion


    }
}
