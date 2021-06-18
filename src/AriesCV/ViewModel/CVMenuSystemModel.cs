using System;
using System.Linq;
using AriesCV.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HandyControl.Controls;

namespace AriesCV.ViewModel
{
    public class CVMenuSystemModel : ViewModelBase
    {

        private int ID { set; get; } = 1;

        #region  File Syetem 命令

        public RelayCommand OpenGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(OpenCVWorkerItem)).Value;

        public RelayCommand NewGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(AddCVWorkerItem)).Value;

        public RelayCommand CloseGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(CloseCVWorkerItem)).Value;


        public RelayCommand CloseAllGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(CloseAllGraphCVFile)).Value;


        public RelayCommand SaveGraphCVFileCommand => new RelayCommand(SaveCVWorkerItem, CanSaveCVWorkerItem);

        public RelayCommand SaveAsGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(SaveCVWorkerItemAs)).Value;

        public RelayCommand SaveAsGraphCVPNGCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(SaveCVWorkerItemAsPng)).Value;

        public CVWorkerContainerModel CvWorkerContainerModel => ViewModelLocator.Instance.CvWorkerContainerModel;

        public GraphCVArea GraphCvAreaWorker => CvWorkerContainerModel.GraphCvAreaWorking;

        private void OpenCVWorkerItem()
        {
            var workModel = CVWorkerItemView.OpenFromAriesFile();
            if (CvWorkerContainerModel.CVWorkerItems.Count(a => a.Name == workModel.Name) >= 1)
            {
                Growl.Error($"Has opened{workModel.Name} Graph CV");
                return;
            }

            CvWorkerContainerModel.CVWorkerItems.Add(workModel);
            CvWorkerContainerModel.CVWorkerItem = workModel;
            Growl.Success($"Open {workModel.Name} Graph CV");
        }


        private void AddCVWorkerItem()
        {
            var workModel = new CVWorkerItemModel($"CVWork_{ID}");
            if (CvWorkerContainerModel.CVWorkerItems.Count(a => a.Name == workModel.Name) >= 1)
            {
                Growl.Error($"Has opened{workModel.Name} Graph CV");
                return;
            }

            ID++;
            CvWorkerContainerModel.CVWorkerItems.Add(workModel);
            CvWorkerContainerModel.CVWorkerItem = workModel;
            Growl.Success($"Open {workModel.Name} Graph CV");
        }

        private void CloseAllGraphCVFile()
        {
            CvWorkerContainerModel.CVWorkerItems.Clear();
            Growl.Success($"Close All Graph CV");
        }

        private void CloseCVWorkerItem()
        {
            var currentItem = CvWorkerContainerModel.CVWorkerItem;
            var name = currentItem.Name;
            CvWorkerContainerModel.CVWorkerItems.Remove(currentItem);
            Growl.Success($"Close {name} Graph CV");
        }

        private bool CanSaveCVWorkerItem()
        {
            var fileinfo = CvWorkerContainerModel.CVWorkerItem?.FileInfo;
            return fileinfo != null;
        }

        private void SaveCVWorkerItem()
        {
            //CvWorkerContainerModel.CVWorkerItem.SaveToSelf();
        }

        private void SaveCVWorkerItemAs()
        {
            //CvWorkerContainerModel.CVWorkerItem.SaveToAriesFile();
        }

        private void SaveCVWorkerItemAsPng()
        {
            //CvWorkerContainerModel.CVWorkerItem.SaveToPicture();
        }

        #endregion



        #region Graph CV 命令

        #endregion

    }
}
