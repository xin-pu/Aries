using System.Windows;
using System.Windows.Controls;

namespace Aries.Controls
{
    public class ToolKitButton : Button
    {
        static ToolKitButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToolKitButton),
                new FrameworkPropertyMetadata(typeof(ToolKitButton)));
        }



    }
}
