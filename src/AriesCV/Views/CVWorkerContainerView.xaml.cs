using System.Collections.Generic;
using Aries.OpenCV.GraphModel;
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


        public GraphCVArea GraphCVArea { set; get; }

        private void RegisterMessenger()
        {
            Messenger.Default.Register<BlockVertex>(this, "AddBlockToken", AddBlockVertex);
            Messenger.Default.Register<CVWorkerItemView>(this, "AddCVWorkerToken", AddCVWorker);
            Messenger.Default.Register<string>(this, "RemoveCVWorkerToken", RemoveCVWorker);
            Messenger.Default.Register<string>(this, "RemoveAllCVWorkerToken", RemoveAllCVWorker);
        }

        private void AddBlockVertex(BlockVertex obj)
        {
            GraphCVArea?.AddBlock(obj);
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
            GraphCVArea = workerItem.GraphCVArea;
            Messenger.Default.Send(workerItem, "AddCVWorkerModelToken");
        }

        public void RemoveCVWorker(string workerName)
        {
            var selectItem = (TabItem) GraphCVTabs.SelectedItem;
            if(selectItem.Name!=workerName)
                return;

            GraphCVTabs.Items.Remove(selectItem);

            Messenger.Default.Send(workerName, "RemoveCVWorkerModelToken");
        }

        public void RemoveAllCVWorker(string message)
        {
        
            GraphCVTabs.Items.Clear();

            Messenger.Default.Send(string.Empty, "RemoveAllCVWorkerModelToken");
        }



        //private childItem FindVisualChild<childItem>(DependencyObject obj)
        //    where childItem : DependencyObject
        //{


        //    for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(obj, i);

        //        if (child is childItem item)
        //        {
        //            return item;
        //        }
        //        else
        //        {
        //            var childOfChild = FindVisualChild<childItem>(child);
        //            if (childOfChild != null)
        //            {
        //                return childOfChild;
        //            }
        //        }
        //    }

        //    return null;
        //}

        //private List<childItem> FindVisualChilds<childItem>(DependencyObject obj)
        //    where childItem : DependencyObject
        //{

        //    var res = new List<childItem>();
        //    for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(obj, i);

        //        if (child is childItem item)
        //        {
        //            res.Add(item);
        //        }
        //        else
        //        {
        //            var childOfChild = FindVisualChilds<childItem>(child);
        //            if (childOfChild != null)
        //            {
        //                res.AddRange(childOfChild);
        //            }
        //        }
        //    }

        //    return res;
        //}


    }
}