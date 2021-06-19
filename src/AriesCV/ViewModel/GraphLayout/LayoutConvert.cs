using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AriesCV.ViewModel.GraphLayout
{
    public class LayoutConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            var layoutType = (LayoutType) value;
            return LayoutCategory.GetLayoutCategory(layoutType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            var layoutType = (LayoutCategory)value;
            return layoutType.LayoutType;
        }
    }



}
