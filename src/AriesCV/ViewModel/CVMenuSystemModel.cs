using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AriesCV.ViewModel
{
    public class CVMenuSystemModel : ViewModelBase
    {

        #region  File Syetem 命令

        public RelayCommand OpenGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(OpenGraphCVFile)).Value;

        public RelayCommand NewGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(NewGraphCVFile)).Value;

        public RelayCommand CloseGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(CloseGraphCVFile)).Value;


        public RelayCommand CloseAllGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(CloseAllGraphCVFile)).Value;


        public RelayCommand SaveGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(SaveGraphCVFile)).Value;

        public RelayCommand SaveAsGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(SaveAsGraphCVFile)).Value;

        public RelayCommand SaveAsGraphCVPNGCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(SaveAsGraphCVPNG)).Value;


        private void OpenGraphCVFile()
        {

        }

        private void NewGraphCVFile()
        {
            var workerModel = ViewModelLocator.Instance.CVWorkerModel;
            var newWorkItem = new CVWorkItemModel
            {
                Name="Default_1",
            };
            workerModel.GraphCVWorkItems.Add(newWorkItem);
            workerModel.GraphCVSelected = newWorkItem;
        }

        private void CloseGraphCVFile()
        {
            var workerModel = ViewModelLocator.Instance.CVWorkerModel;
            workerModel.GraphCVWorkItems.Remove(workerModel.GraphCVSelected);
        }

        private void CloseAllGraphCVFile()
        {
            ViewModelLocator.Instance.CVWorkerModel.GraphCVWorkItems.Clear();
        }

        private void SaveGraphCVFile()
        {
           
        }

        private void SaveAsGraphCVFile()
        {

        }

        private void SaveAsGraphCVPNG()
        {

        }

        #endregion




    }
}
