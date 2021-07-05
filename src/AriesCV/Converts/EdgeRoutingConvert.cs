using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Aries.OpenCV.GraphModel;
using AriesCV.ViewModel.Menu;

namespace AriesCV.Converts
{
    public class EdgeRoutingConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            var edgeRoutingType = (EdgeRoutingType)value;
            return EdgeRoutingCategory.GetEdgeRoutingCategory(edgeRoutingType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            var layoutType = (EdgeRoutingCategory)value;
            return layoutType.EdgeRoutingType;
        }
    }
}
