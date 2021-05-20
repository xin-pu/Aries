using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Aries
{
    [ValueConversion(typeof(Color), typeof(SolidColorBrush))]
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color) value;
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var version = (SolidColorBrush) (value);
                return version.Color;
            }
            return Colors.Transparent;
        }
    }
}