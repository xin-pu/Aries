using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AriesCV.ViewModel
{
    public class CVMenuSystemModel : ViewModelBase
    {

        #region 命令

        public RelayCommand OpenGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(OpenGraphCVFile)).Value;

        public RelayCommand NewGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(NewGraphCVFile)).Value;


        public RelayCommand SaveGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(SaveGraphCVFile)).Value;

        public RelayCommand SaveAsGraphCVFileCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(SaveAsGraphCVFile)).Value;

        #endregion




        #region 方法

        private void OpenGraphCVFile()
        {

        }

        private void NewGraphCVFile()
        {

        }

        private void SaveGraphCVFile()
        {

        }

        private void SaveAsGraphCVFile()
        {

        }

        #endregion
    }
}
