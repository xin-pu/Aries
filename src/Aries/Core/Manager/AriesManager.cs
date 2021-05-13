using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Aries.Core
{
    public class AriesManager : INotifyPropertyChanged
    {

        public ObservableCollection<GraphCVManager> _logicCoreCvs;

        public GraphCVManager _logicCoreCvSelect;

        public ObservableCollection<GraphCVManager> LogicCoreCvs
        {
            set { UpdateProperty(ref _logicCoreCvs, value); }
            get { return _logicCoreCvs; }
        }

        public GraphCVManager LogicCoreCvSelect
        {
            set { UpdateProperty(ref _logicCoreCvSelect, value); }
            get { return _logicCoreCvSelect; }
        }

        private static readonly Lazy<AriesManager> lazy =
            new Lazy<AriesManager>(() => new AriesManager());

        public static AriesManager Instance
        {
            get { return lazy.Value; }
        }

        public AriesManager()
        {
            LogicCoreCvs = new ObservableCollection<GraphCVManager>();
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
