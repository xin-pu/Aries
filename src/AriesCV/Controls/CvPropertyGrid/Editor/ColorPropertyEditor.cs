using System.Windows;
using HandyControl.Controls;

namespace AriesCV.Controls
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
