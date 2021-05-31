using Aries.Core;

namespace Aries.Views
{
    /// <summary>
    /// Interaction logic for ToolKitUnit.xaml
    /// </summary>
    public partial class ToolKitUnit
    {
        public ToolKitUnit()
        {
            InitializeComponent();
            DataContext = ToolKitManager;
        }

        public ToolKitManager ToolKitManager => ToolKitManager.Instance;


    }
}
