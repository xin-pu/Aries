using System.Windows;

namespace Aries
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Test();
        }
    }

    public class Test
    {
        public HorizontalAlignment HorizontalAlignment { set; get; }
        public bool IsRead { set; get; }
        public Test2 Test2 { set; get; }

    }

    public class Test2
    {
        public int Value { set; get; }
    }
}
