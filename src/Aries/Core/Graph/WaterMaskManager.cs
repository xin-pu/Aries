using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Aries.Core
{
    public class WaterMaskManager : INotifyPropertyChanged
    {
        private string _mask = "Aries";
        private int _fontSize = 100;
        private double _opactiy = 0.2;
        private double _angle = 45;
        private Brush _brush = new SolidColorBrush(Colors.Gray);
        private Thickness _maskMargin = new Thickness(30);

        public string Mask
        {
            set { UpdateProperty(ref _mask, value); }
            get { return _mask; }
        }

        public int FonnSize
        {
            set { UpdateProperty(ref _fontSize, value); }
            get { return _fontSize; }
        }

        public double Opactiy
        {
            set { UpdateProperty(ref _opactiy, value); }
            get { return _opactiy; }
        }

        public double Angle
        {
            set { UpdateProperty(ref _angle, value); }
            get { return _angle; }
        }

        public Brush Brush
        {
            set { UpdateProperty(ref _brush, value); }
            get { return _brush; }
        }

        public Thickness MaskMargin
        {
            set { UpdateProperty(ref _maskMargin, value); }
            get { return _maskMargin; }
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
