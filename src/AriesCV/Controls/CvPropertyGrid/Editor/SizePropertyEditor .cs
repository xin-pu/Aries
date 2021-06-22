using System.Windows;
using HandyControl.Controls;

namespace AriesCV.Controls
{
    public class SizePropertyEditor : PropertyEditorBase
    {
        public override FrameworkElement CreateElement(PropertyItem propertyItem)
        {
            return new CvSizeEditor();
        }

        public override DependencyProperty GetDependencyProperty() => CvSizeEditor.SizeProperty;

    }
}
