using GalaSoft.MvvmLight.Messaging;
using HandyControl.Controls;
using System.Windows;

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

        private void RegisterMessenger()
        {
            Messenger.Default.Register<CVWorkerItemView>(this, "AddCVWorkerToken", AddCVWorker);
            Messenger.Default.Register<string>(this, "RemoveCVWorkerToken", RemoveCVWorker);
            Messenger.Default.Register<string>(this, "RemoveAllCVWorkerToken", RemoveAllCVWorker);
        }


        public void AddCVWorker(CVWorkerItemView workerItem)
        {
            var tabItem = new TabItem
            {
                Name = workerItem.Name,
                Header = workerItem.Name,
                Content = workerItem
            };
            GraphCVTabs.Items.Add(tabItem);
            GraphCVTabs.SelectedItem = tabItem;
            Messenger.Default.Send(workerItem, "AddCVWorkerModelToken");
        }


        public void RemoveCVWorker(string workerName)
        {
            var selectItem = (TabItem)GraphCVTabs.SelectedItem;
            if (selectItem?.Name != workerName)
                return;

            GraphCVTabs.Items.Remove(selectItem);

            Messenger.Default.Send(workerName, "RemoveCVWorkerModelToken");
        }

        public void RemoveAllCVWorker(string message)
        {

            GraphCVTabs.Items.Clear();
            Messenger.Default.Send(string.Empty, "RemoveAllCVWorkerModelToken");
        }


        private void TabItemClosed(object sender, System.EventArgs e)
        {
            var itemCurrent = (TabItem)((RoutedEventArgs)e).Source;
            Messenger.Default.Send(itemCurrent.Name, "RemoveCVWorkerModelToken");
        }
    }
}