using System.Windows;
using HandyControl.Controls;

namespace Aries.Control.CvControl.Editor
{
    public class ColorPropertyEditor : PropertyEditorBase
    {
        public override FrameworkElement CreateElement(PropertyItem propertyItem)
        {
            return new ColorPicker();
        }

        public override DependencyProperty GetDependencyProperty() => ColorPicker.SelectedBrushProperty;

    }
}
