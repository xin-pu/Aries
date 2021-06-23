using System.Windows;
using HandyControl.Controls;
using HandyControl.Tools;

namespace AriesCV.Controls
{
    public class MatPropertyEditor : PropertyEditorBase
    {
        public override FrameworkElement CreateElement(PropertyItem propertyItem)
        {
            return new CvMatEditor
            {
                Content = propertyItem.PropertyName,
                Style = ResourceHelper.GetResource<Style>("LabelInfo"),
                HorizontalAlignment = HorizontalAlignment.Left
            };
        }


        public override DependencyProperty GetDependencyProperty() => CvMatEditor.MatProperty;

    }
}
