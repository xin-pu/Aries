using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace AriesCV.Views
{
    /// <summary>
    /// Interaction logic for GraphCVToolKitView.xaml
    /// </summary>
    public partial class CVToolKitView
    {
        public CVToolKitView()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(new Action(() =>
            {
                GraphListView.Items.GroupDescriptions?.Add(new PropertyGroupDescription("Category"));
            }), DispatcherPriority.Background);
           
        }


        private void TreeViewItemExpanded(object sender, RoutedEventArgs e)
        {
            var treeView = (TreeView) sender;
            var currentTreeViewItem = (TreeViewItem) e.OriginalSource;
            foreach (var item in treeView.Items)
            {
                var itm = (TreeViewItem) treeView.ItemContainerGenerator.ContainerFromItem(item);
                if (itm != null && itm != currentTreeViewItem)
                {
                    itm.IsExpanded = false;
                }

            }
        }

       
    }
}
