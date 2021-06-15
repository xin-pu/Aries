using System.Windows;
using System.Windows.Controls;

namespace AriesCV.Controls
{

    public class TopTabButton : Button
    {
        static TopTabButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TopTabButton),
                new FrameworkPropertyMetadata(typeof(TopTabButton)));
        }

        public string Icon
        {
            get { return (string) GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(TopTabButton), new PropertyMetadata(null));


    }
}
