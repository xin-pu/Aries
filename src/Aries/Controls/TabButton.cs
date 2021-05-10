using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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


        public static T FindVisualParent<T>(DependencyObject obj) where T : class
        {
            while (obj != null)
            {
                if (obj is T)
                    return obj as T;

                obj = VisualTreeHelper.GetParent(obj);
            }

            return null;
        }
    }


}
