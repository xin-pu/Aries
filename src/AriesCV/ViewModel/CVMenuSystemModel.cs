using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

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

        public CVWorkerContainerModel CvWorkerContainerModel => ViewModelLocator.Instance.CvWorkerContainerModel;

        public GraphCVArea GraphCvAreaWorker => CvWorkerContainerModel.GraphCvAreaWorking;

        private void OpenCVWorkerItem()
        {

        }
        int id = 0;
        private void AddCVWorkerItem()
        {
            var workModel = new CVWorkerItemModel($"CVWork{id}");
            CvWorkerContainerModel.CVWorkerItems.Add(workModel);
            CvWorkerContainerModel.CVWorkerItem = workModel;
            id++;
        }

        private void CloseAllGraphCVFile()
        {
            CvWorkerContainerModel.CVWorkerItems.Clear();
        }

        private void CloseCVWorkerItem()
        {
            CvWorkerContainerModel.CVWorkerItems.Remove(CvWorkerContainerModel.CVWorkerItem);
        }

        private bool CanSaveCVWorkerItem()
        {
            return true;
        }

        private void SaveCVWorkerItem()
        {

        }

        private void SaveCVWorkerItemAs()
        {

        }

        private void SaveCVWorkerItemAsPng()
        {

        }

        #endregion



        #region Graph CV 命令

        #endregion

    }
}
