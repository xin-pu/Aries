using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Aries.Core
{
    [Serializable]
    public class BackGroundManager : INotifyPropertyChanged
    {
        private double _opactiy = 1;
        private Brush _brush = new SolidColorBrush(Colors.Transparent);

        [XmlIgnore]
        public Brush Brush
        {
            set { UpdateProperty(ref _brush, value); }
            get { return _brush; }
        }

        public double Opactiy
        {
            set { UpdateProperty(ref _opactiy, value); }
            get { return _opactiy; }
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
