using System.Windows;
using System.Windows.Controls;

namespace Aries.Controls
{

    public class TabButton : Button
    {
        static TabButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabButton),
                new FrameworkPropertyMetadata(typeof(TabButton)));
        }

        public string Icon
        {
            get { return (string) GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(TabButton), new PropertyMetadata(null));


    }




}
