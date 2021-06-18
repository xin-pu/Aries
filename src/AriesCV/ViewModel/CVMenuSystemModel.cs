using System;
using AriesCV.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
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


        public RelayCommand SaveGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(SaveCVWorkerItem)).Value;
        

        public RelayCommand SaveAsGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(SaveCVWorkerItemAs)).Value;

        public RelayCommand SaveAsGraphCVPNGCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(SaveCVWorkerItemAsPng)).Value;

        public CVWorkerContainerModel CvWorkerContainerModel => ViewModelLocator.Instance.CvWorkerContainerModel;



        private void OpenCVWorkerItem()
        {
            var workModel = CVWorkerItemView.OpenFromAriesFile();
            if (workModel == null)
            {
                Growl.Error($"Fail to Open Graph CV");
                return;
            }

            if (CvWorkerContainerModel.CurrentKeys.Contains(workModel.Name))
            {
                Growl.Error($"Has opened{workModel.Name} Graph CV");
                return;
            }

            Messenger.Default.Send(workModel, "AddCVWorkerToken");
            Growl.Success($"Open {workModel.Name} Graph CV");
        }


        private void AddCVWorkerItem()
        {
            var workModel = new CVWorkerItemView($"CVWork_{ID}");

            if (CvWorkerContainerModel.CurrentKeys.Contains(workModel.Name))
            {
                Growl.Error($"Has opened{workModel.Name} Graph CV");
                return;
            }

            ID++;
            Messenger.Default.Send(workModel, "AddCVWorkerToken");
            Growl.Success($"Open {workModel.Name} Graph CV");
        }

        private void CloseAllGraphCVFile()
        {
            Messenger.Default.Send(string.Empty, "RemoveAllCVWorkerToken");
            ID = 1;
        }

        private void CloseCVWorkerItem()
        {
            Messenger.Default.Send(CvWorkerContainerModel?.CvWorkerItemView?.Name, "RemoveCVWorkerToken"); 
        }

        private void SaveCVWorkerItem()
        {
            CvWorkerContainerModel?.CvWorkerItemView?.SaveToSelf();
        }

        private void SaveCVWorkerItemAs()
        {
            CvWorkerContainerModel?.CvWorkerItemView?.SaveToAriesFile();

        }

        private void SaveCVWorkerItemAsPng()
        {
            CvWorkerContainerModel?.CvWorkerItemView?.SaveToPicture();
        }

        #endregion



        #region Graph CV 命令

        #endregion

    }
}
