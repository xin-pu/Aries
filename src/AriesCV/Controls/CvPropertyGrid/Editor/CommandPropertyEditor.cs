using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using HandyControl.Controls;
using HandyControl.Tools;

namespace AriesCV.Controls
{
    public class CommandPropertyEditor : PropertyEditorBase
    {
        public override FrameworkElement CreateElement(PropertyItem propertyItem)
        {
            return new Button
            {
                Content = propertyItem.PropertyName,
                Style = ResourceHelper.GetResource<Style>("ButtonDashedPrimary"),
                HorizontalAlignment = HorizontalAlignment.Left
            };
        }

        public override DependencyProperty GetDependencyProperty() => ButtonBase.CommandProperty;

    }
}
