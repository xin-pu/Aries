using System.Windows;
using HandyControl.Controls;

namespace AriesCV.Controls
{
    public class RectPropertyEditor : PropertyEditorBase
    {
        public override FrameworkElement CreateElement(PropertyItem propertyItem)
        {
            return new CvRectEditor();
        }

        public override DependencyProperty GetDependencyProperty() => CvRectEditor.RectProperty;

    }
}
