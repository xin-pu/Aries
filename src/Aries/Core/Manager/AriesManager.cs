using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Aries.Core
{
    public class AriesManager : INotifyPropertyChanged
    {

        public ObservableCollection<GraphCVCore> _graphCvCores;

        public GraphCVCore _graphCvCore;

        public ObservableCollection<GraphCVCore> GraphCvCores
        {
            set { UpdateProperty(ref _graphCvCores, value); }
            get { return _graphCvCores; }
        }

        public GraphCVCore GraphCvCore
        {
            set { UpdateProperty(ref _graphCvCore, value); }
            get { return _graphCvCore; }
        }

        private static readonly Lazy<AriesManager> lazy =
            new Lazy<AriesManager>(() => new AriesManager());

        public static AriesManager Instance
        {
            get { return lazy.Value; }
        }

        public AriesManager()
        {
            GraphCvCores = new ObservableCollection<GraphCVCore>();
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
