using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Aries.OpenCV.GraphModel;
using Aries.Utility;

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

        public ICommand ClearRecordsCommand
        {
            get { return new RelayCommand(ClearRecords); }
        }


        public void AppendMatRecords(List<MatRecord> matRecords)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                matRecords.ForEach(matRecord => MatRecords.Add(matRecord));
            });
        }


        public void ClearRecords()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MatRecords.Clear();
            });
        }


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
