using System.Windows;
using HandyControl.Controls;

namespace AriesCV.Controls
{

    public class ScalarPropertyEditor : PropertyEditorBase
    {
        public override FrameworkElement CreateElement(PropertyItem propertyItem)
        {
            return new CvScalarEditor();
        }

        public override DependencyProperty GetDependencyProperty() => CvScalarEditor.ScalarProperty;

    }
}
