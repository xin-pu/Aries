using System;
using System.IO;
using Aries.OpenCV.GraphModel;
using AriesCV.Controls;
using AriesCV.ViewModel;
using AriesCV.ViewModel.GraphCV;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using HandyControl.Controls;
using Microsoft.Win32;

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
            RegisterMessenger();
            DataContext = this;
        }

        #region  属性

        
        public GraphCVArea GraphCvAreaAtWorkSpace { set; get; }
        public CVWorkerItemView CvWorkerItem { set; get; }

        #endregion


        private void RegisterMessenger()
        {
            Messenger.Default.Register<string>(this, "OpenCVWorkerItemToken", OpenCVWorkerItem);
            Messenger.Default.Register<string>(this, "AddCVWorkerItemToken", AddCVWorkerItem);
            Messenger.Default.Register<string>(this, "CloseCVWorkerItemToken", CloseCVWorkerItem);
            Messenger.Default.Register<string>(this, "CloseAllCVWorkerItemToken", CloseAllCVWorkerItem);

            Messenger.Default.Register<bool>(this, "SaveCVWorkerItemToken", SaveCVWorkerItem);
            Messenger.Default.Register<bool>(this, "SaveCVWorkerItemAsToken", SaveCVWorkerItemAs);
            Messenger.Default.Register<bool>(this, "SaveCVWorkerItemAsPngToken", SaveCVWorkerItemAsPng);

            
            Messenger.Default.Register<BlockVertex>(this, "AddBlockToken", AddBlockVertex);
        }


        private void OpenCVWorkerItem(string obj)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = @"aries(*.ar)|*.ar",
            };
            openFileDialog.ShowDialog();

            if (File.Exists(openFileDialog.FileName))
            {
                try
                {
                    var graphCVFile = GraphCVFileStruct.DeserializeGraphDataFromFile(openFileDialog.FileName);
                    var fileInfo = new FileInfo(openFileDialog.FileName);

                    var panel = new CVWorkerItemView
                    {
                        GraphCVArea = {Name = fileInfo.Name},
                        FileInfo = fileInfo,
                        WorkDirectory = fileInfo.DirectoryName
                    };

                    var grapArea = panel.GraphCVArea;
                    grapArea.RebuildFromSerializationData(graphCVFile.GraphSerializationDatas);
                    grapArea.SetVerticesDrag(true, true);
                    grapArea.UpdateAllEdges();

                    panel.ZoomControl.ZoomToFill();

                    var tabItem = new TabItem
                    {
                        Header = panel.GraphCVArea.Name,
                        Content = panel,
                    };
                    GraphCVTabs.Items.Add(tabItem);
                    GraphCVTabs.SelectedItem = tabItem;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AddCVWorkerItem(string graphName)
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

        private void CloseAllCVWorkerItem(string obj)
        {
            GraphCVTabs.Items.Clear();
        }

        private void CloseCVWorkerItem(string obj)
        {
            var selectItem = GraphCVTabs.SelectedItem;
            GraphCVTabs.Items.Remove(selectItem);
        }

        private void AddBlockVertex(BlockVertex obj)
        {
            GraphCvAreaAtWorkSpace?.AddBlock(obj);
        }

        private void SaveCVWorkerItem(bool reload)
        {
            if (reload)
            {
                GraphCvAreaAtWorkSpace.ReloadBlocks();
            }

            GraphCVFileStruct.SerializeGraphDataToFile(CvWorkerItem.FileInfo.FullName,
                new GraphCVFileStruct
                {
                    GraphSerializationDatas = GraphCvAreaAtWorkSpace.ExtractSerializationData(),
                });
        }


        private void SaveCVWorkerItemAsPng(bool reload)
        {

            if (reload)
            {
                GraphCvAreaAtWorkSpace.ReloadBlocks();
            }

            GraphCVFileStruct.SerializeGraphDataToFile(CvWorkerItem.FileInfo.FullName,
                new GraphCVFileStruct
                {
                    GraphSerializationDatas = GraphCvAreaAtWorkSpace.ExtractSerializationData(),
                });
        }

        private void SaveCVWorkerItemAs(bool reload)
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = @"aries(*.ar)|*.ar",
            };
            var res = saveDialog.ShowDialog();
            if (res != true || saveDialog.FileName == "") return;

            if (reload)
            {
                GraphCvAreaAtWorkSpace.ReloadBlocks();
            }

            GraphCVFileStruct.SerializeGraphDataToFile(saveDialog.FileName,
                new GraphCVFileStruct
                {
                    GraphSerializationDatas = GraphCvAreaAtWorkSpace.ExtractSerializationData(),
                });
        }





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

        private void GraphCVTabs_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var model = ViewModelLocator.Instance.CVWorkerModel;
            model.GraphCvAreaAtWorkSpace = GraphCvAreaAtWorkSpace;
            model.CvWorkerItem = CvWorkerItem;
        }


        #endregion


    }
}
