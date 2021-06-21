using System.Windows;
using HandyControl.Controls;

namespace AriesCV.Controls
{
    public class PointPropertyEditor : PropertyEditorBase
    {
        public override FrameworkElement CreateElement(PropertyItem propertyItem)
        {
            return new CvPointEditor();
        }

        public override DependencyProperty GetDependencyProperty() => CvPointEditor.PointProperty;

    }
}
