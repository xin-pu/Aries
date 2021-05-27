using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Aries.Core
{

    public class BackGroundManager : INotifyPropertyChanged
    {
        private double _opactiy = 1;
        private Color _brush = Colors.Transparent;


        public Color Brush
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
