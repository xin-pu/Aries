using System;
using System.IO;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;

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
            var openFileDialog = new OpenFileDialog
            {
                Filter = @"aries(*.ar)|*.ar",
            };
            openFileDialog.ShowDialog();
            if (!File.Exists(openFileDialog.FileName))
                return;


            Messenger.Default.Send(openFileDialog.FileName, "OpenCVWorkerItemToken");
        }

        private void NewGraphCVFile()
        {
            Messenger.Default.Send("Default", "AddCVWorkerItemToken");
        }

        private void CloseGraphCVFile()
        {
            Messenger.Default.Send("Default", "CloseCVWorkerItemToken");
        }

        private void CloseAllGraphCVFile()
        {
            Messenger.Default.Send("Default", "CloseAllCVWorkerItemToken");
        }

        private void SaveGraphCVFile()
        {
            Messenger.Default.Send(true, "SaveCVWorkerItemToken");
        }

        private void SaveAsGraphCVFile()
        {
            Messenger.Default.Send(true, "SaveCVWorkerItemAs");
        }

        private void SaveAsGraphCVPNG()
        {
            Messenger.Default.Send(true, "SaveCVWorkerItemAsPng");
        }

        #endregion




    }
}
