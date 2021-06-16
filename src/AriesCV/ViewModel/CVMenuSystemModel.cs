using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace AriesCV.ViewModel
{
    public class CVMenuSystemModel : ViewModelBase
    {

        public CVMenuSystemModel()
        { 

        }

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
            Messenger.Default.Send("Default", "AddCVWorkerItemToken");
        }

        private void CloseGraphCVFile()
        {
            //var workerModel = ViewModelLocator.Instance.CVWorkerModel;
            //workerModel.GraphCVWorkItems.Remove(workerModel.GraphCVSelected);
        }

        private void CloseAllGraphCVFile()
        {
            //ViewModelLocator.Instance.CVWorkerModel.GraphCVWorkItems.Clear();
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
