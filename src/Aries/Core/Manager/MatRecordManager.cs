using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Aries.OpenCV.GraphModel;
using Aries.Utility;
using GraphX.Common;

namespace Aries.Core
{
    public class MatRecordManager : INotifyPropertyChanged
    {
        private ObservableCollection<MatRecord> _matRecords;


        public ObservableCollection<MatRecord> MatRecords
        {
            set { UpdateProperty(ref _matRecords, value); }
            get { return _matRecords; }
        }


        public MatRecordManager()
        {
            MatRecords=new ObservableCollection<MatRecord>();
        }

        public void AppendMatRecords(List<MatRecord> matRecords)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                matRecords.ForEach(matRecord => MatRecords.Add(matRecord));
            });
        }



        #region


        public ICommand ClearRecordsCommand
        {
            get { return new RelayCommand(ClearRecords); }
        }

        public void ClearRecords()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MatRecords.ForEach(a => a.Dispose());
                MatRecords.Clear();
            });
        }


        public ICommand SortByIdCommand
        {
            get { return new RelayCommand(SortByIdCommand_Execute); }
        }

        private void SortByIdCommand_Execute()
        {
            MatRecords = new ObservableCollection<MatRecord>(MatRecords.OrderBy(a => a.ParentId));
        }

        public ICommand SortByNameCommand
        {
            get { return new RelayCommand(SortByNameCommand_Execute); }
        }

        private void SortByNameCommand_Execute()
        {
            MatRecords = new ObservableCollection<MatRecord>(MatRecords.OrderBy(a => a.ParentName));
        }

        public ICommand SortByTimeCommand
        {
            get { return new RelayCommand(SortByTimeCommand_Execute); }
        }

        private void SortByTimeCommand_Execute()
        {
            MatRecords = new ObservableCollection<MatRecord>(MatRecords.OrderBy(a => a.UpDateTime));
        }

        #endregion




        #region

        internal void UpdateProperty<T>(ref T properValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(properValue, newValue))
            {
                return;
            }

            properValue = newValue;

            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }



}
