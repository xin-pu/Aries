using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Aries.Core;
using Aries.Utility;
using Aries.Views;

namespace Aries
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AriesMain : INotifyPropertyChanged
    {
        public AriesMain()
        {
            InitializeComponent();
            DataContext = this;
            FileSystemManager.AriesMain = this;
        }

        private GraphCVCore _graphCvCoreAtWorkSpace;
        public FileSystemManager FileSystemManager => FileSystemManager.Instance;


        public GraphCVCore GraphCvCoreAtWorkSpace
        {
            set { UpdateProperty(ref _graphCvCoreAtWorkSpace, value); }
            get { return _graphCvCoreAtWorkSpace; }
        }



        #region Command

        public ICommand SelectWorkUnitCommand
        {
            get { return new RelayCommand(SelectWorkUnitCommand_Execute); }
        }

        private void SelectWorkUnitCommand_Execute()
        {
            GraphCvCoreAtWorkSpace = ((AriesCoreUint) WorkSpace.SelectedContent).GraphCvCore;
            ToolKitManager.Instance.FreshGraphCvCoreAtWorkSpace(GraphCvCoreAtWorkSpace);
        }


        #endregion



        #region

        internal void UpdateProperty<T>(ref T properValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(properValue, newValue))
            {
                return;
            }

            properValue = newValue;

            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }


}
