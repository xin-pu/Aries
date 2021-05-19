using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Aries.Views;

namespace Aries.Core
{
    public class AriesManager : INotifyPropertyChanged
    {



        private static readonly Lazy<AriesManager> lazy =
            new Lazy<AriesManager>(() => new AriesManager());

        public static AriesManager Instance
        {
            get { return lazy.Value; }
        }

        public AriesManager()
        {
            AriesCoreUints = new ObservableCollection<AriesCoreUint>();
        }


        public ObservableCollection<AriesCoreUint> _ariesCoreUints;
        public AriesCoreUint _ariesCoreUint;

        public MainWindow MainWindow { set; get; }


        public ObservableCollection<AriesCoreUint> AriesCoreUints
        {
            set { UpdateProperty(ref _ariesCoreUints, value); }
            get { return _ariesCoreUints; }
        }

        public AriesCoreUint AriesCoreUint
        {
            set { UpdateProperty(ref _ariesCoreUint, value); }
            get { return _ariesCoreUint; }
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
