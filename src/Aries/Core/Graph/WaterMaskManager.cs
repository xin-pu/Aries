using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using Aries.Utility;

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
        private bool _isOpen = true;

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
        
        [XmlIgnore]
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

        public bool IsOpen
        {
            set { UpdateProperty(ref _isOpen, value); }
            get { return _isOpen; }
        }

        [XmlIgnore]
        public ICommand RemoveWaterMaskCommand
        {
            get { return new RelayCommand(RemoveWaterMaskCommand_Execute); }
        }


        private void RemoveWaterMaskCommand_Execute()
        {
            IsOpen = !IsOpen;
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
