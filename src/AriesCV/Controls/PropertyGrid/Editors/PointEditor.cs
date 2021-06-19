using System.Windows;
using System.Windows.Controls;
using HandyControl.Controls;

namespace AriesCV.Controls.PropertyGrid.Editors
{
    public class PointEditor : PropertyEditorBase
    {
        public override FrameworkElement CreateElement(PropertyItem propertyItem)
        {
            var res = new StackPanel();

            return res;
        }

        public override DependencyProperty GetDependencyProperty() => ColorPicker.SelectedBrushProperty;
    }



}
