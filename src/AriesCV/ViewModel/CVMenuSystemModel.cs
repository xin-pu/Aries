using System;
using System.IO;
using AriesCV.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GraphX.Common.Enums;
using HandyControl.Controls;
using Microsoft.Win32;

namespace AriesCV.ViewModel
{
    public class CVMenuSystemModel : ViewModelBase
    {



        #region  File Syetem 命令

        public RelayCommand OpenGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(OpenCVWorkerItem)).Value;

        public RelayCommand NewGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(AddCVWorkerItem)).Value;

        public RelayCommand CloseGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(CloseCVWorkerItem)).Value;


        public RelayCommand CloseAllGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(CloseAllGraphCVFile)).Value;


        public RelayCommand SaveGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(SaveCVWorkerItem, CanSaveCVWorkerItem)).Value;

        public RelayCommand SaveAsGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(SaveCVWorkerItemAs)).Value;

        public RelayCommand SaveAsGraphCVPNGCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(SaveCVWorkerItemAsPng)).Value;


        public GraphCVArea GraphCvAreaAtWorkSpace => ViewModelLocator.Instance.CVWorkerModel.GraphCvAreaWorking;

        public CVWorkerItemView CvWorkerItem => ViewModelLocator.Instance.CVWorkerModel.CvWorkerItem;

        private void OpenCVWorkerItem()
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
                    var panel = new CVWorkerItemView(graphCVFile.GraphCVConfig)
                    {
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
                        Header = fileInfo.Name,
                        Content = panel,
                    };
                    Messenger.Default.Send(tabItem, "AddNewTabToken");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AddCVWorkerItem()
        {
            var panel = new CVWorkerItemView(new GraphCVConfig())
            {
                GraphCVArea =
                {
                    Name = "Default"
                }
            };
            var tabItem = new TabItem
            {
                Header = panel.GraphCVArea.Name,
                Content = panel,
            };
            Messenger.Default.Send(tabItem, "AddNewTabToken");
        }

        private void CloseAllGraphCVFile()
        {
            Messenger.Default.Send(string.Empty, "ClearTabToken");
        }

        private void CloseCVWorkerItem()
        {
            Messenger.Default.Send(string.Empty, "RemoveTabToken");
        }

        private bool CanSaveCVWorkerItem()
        {
            return CvWorkerItem?.FileInfo != null;
        }

        private void SaveCVWorkerItem()
        {
            GraphCvAreaAtWorkSpace.ReloadBlocks();

            GraphCVFileStruct.SerializeGraphDataToFile(CvWorkerItem.FileInfo.FullName,
                GraphCvAreaAtWorkSpace.GetCvFileStruct());
        }

        private void SaveCVWorkerItemAs()
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = @"aries(*.ar)|*.ar",
            };
            var res = saveDialog.ShowDialog();
            if (res != true || saveDialog.FileName == "") return;

            GraphCvAreaAtWorkSpace.ReloadBlocks();

            GraphCVFileStruct.SerializeGraphDataToFile(saveDialog.FileName,
                GraphCvAreaAtWorkSpace.GetCvFileStruct());
        }

        private void SaveCVWorkerItemAsPng()
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = @"png(*.png)|*.png",
            };
            var res = saveDialog.ShowDialog();
            if (res != true || saveDialog.FileName == "") return;

            GraphCvAreaAtWorkSpace.ReloadBlocks();

            GraphCvAreaAtWorkSpace.ExportAsImage(saveDialog.FileName, ImageType.JPEG);
        }

        #endregion

    


    }
}
