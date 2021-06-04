using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Aries.Core
{
    public class WaterMaskManager : INotifyPropertyChanged
    {
        private string _mask;
        private int _fontSize;
        private double _opactiy;
        private double _angle;
        private Color _color;
        private Thickness _maskMargin;
        private bool _isOpen;

        public WaterMaskManager()
        {
            Mask = "Aries";
            FontSize = 100;
            Opacity = 0.2;
            Angle = 45;
            MyColor = Colors.Blue;
            MaskMargin = new Thickness(30);
            IsOpen = true;
        }

        public string Mask
        {
            set { UpdateProperty(ref _mask, value); }
            get { return _mask; }
        }

        public int FontSize
        {
            set { UpdateProperty(ref _fontSize, value); }
            get { return _fontSize; }
        }

        public double Opacity
        {
            set { UpdateProperty(ref _opactiy, value); }
            get { return _opactiy; }
        }

        public double Angle
        {
            set { UpdateProperty(ref _angle, value); }
            get { return _angle; }
        }

        public Color MyColor
        {
            set { UpdateProperty(ref _color, value); }
            get { return _color; }
        }

        public Thickness MaskMargin
        {
            set { UpdateProperty(ref _maskMargin, value); }
            get { return _maskMargin; }
        }

        public bool IsOpen
        {
            set { UpdateProperty(ref _isOpen, value); }
            get { return _isOpen; }
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
