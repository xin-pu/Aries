using Aries.Core;

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
            DataContext = this;
            FileSystemManager.MainWindow = this;
        }
        public AriesManager AriesManager => AriesManager.Instance;
        public FileSystemManager FileSystemManager => FileSystemManager.Instance;
      




       
    }


}
