using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Aries.OpenCV.GraphModel;
using AriesCV.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GraphX.Controls;
using GraphX.Controls.Animations;
using GraphX.Controls.Models;
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

        public ZoomControl ZoomControl { set; get; }




        private void RegisterMessenger()
        {
            Messenger.Default.Register<BlockVertex>(this, "AddBlockToken", AddBlockVertex);
        }

        private void AddBlockVertex(BlockVertex obj)
        {
            GraphCVArea?.AddBlock(obj);
        }



        #region Command

        private async void GraphCVTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var con = GraphCVTabs.SelectedValue as CVWorkerItemModel;
            if (con == null)
                return;
            await Task.Delay(500);
            var borders = FindVisualChilds<SimplePanel>(this);
            var workPanel = borders.FirstOrDefault(a => a.Name == "WorkPanel");
            if (workPanel == null)
                return;
            workPanel.Children.Clear();
            workPanel.Children.Add(con.ZoomControl);
            GraphCVArea = con.GraphCVArea;
        }


        #endregion

        private childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {


            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);

                if (child is childItem item)
                {
                    return item;
                }
                else
                {
                    var childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }

            return null;
        }

        private List<childItem> FindVisualChilds<childItem>(DependencyObject obj)
             where childItem : DependencyObject
        {

            var res = new List<childItem>();
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);

                if (child is childItem item)
                {
                    res.Add(item);
                }
                else
                {
                    var childOfChild = FindVisualChilds<childItem>(child);
                    if (childOfChild != null)
                    {
                        res.AddRange(childOfChild);
                    }
                }
            }

            return res;
        }


    }
}