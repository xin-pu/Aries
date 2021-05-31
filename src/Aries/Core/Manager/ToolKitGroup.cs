using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Aries.Core
{
    public class ToolKitGroup : INotifyPropertyChanged
    {

        private string _groupName;
        private bool _isExpanded = true;
        private ObservableCollection<ToolKitStruct> _toolKitStructs;

        public string GroupName
        {
            set { UpdateProperty(ref _groupName, value); }
            get { return _groupName; }
        }

        public bool IsExpanded
        {
            set { UpdateProperty(ref _isExpanded, value); }
            get { return _isExpanded; }
        }

        public ObservableCollection<ToolKitStruct> ToolKitStructs
        {
            set { UpdateProperty(ref _toolKitStructs, value); }
            get { return _toolKitStructs; }
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