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
            GraphCvRunManager.AriesMain = this;
            GraphStyleManager.AriesMain = this;

        }

        private GraphCVArea _graphCvAreaAtWorkSpace;
        public AriesCoreUint _ariesCoreUint;
        public FileSystemManager FileSystemManager => FileSystemManager.Instance;
        public GraphStyleManager GraphStyleManager => GraphStyleManager.Instance;
        public GraphCVRunManager GraphCvRunManager => GraphCVRunManager.Instance;

        public GraphCVArea GraphCvAreaAtWorkSpace
        {
            set { UpdateProperty(ref _graphCvAreaAtWorkSpace, value); }
            get { return _graphCvAreaAtWorkSpace; }
        }

        public AriesCoreUint AriesCoreUint
        {
            set { UpdateProperty(ref _ariesCoreUint, value); }
            get { return _ariesCoreUint; }
        }

        #region Command

        public ICommand SelectWorkUnitCommand
        {
            get { return new RelayCommand(SelectWorkUnitCommand_Execute, SelectWorkUnitCommand_CanExecute); }
        }

        private bool SelectWorkUnitCommand_CanExecute()
        {
            return WorkSpace.SelectedContent != null;
        }

        private void SelectWorkUnitCommand_Execute()
        {
            AriesCoreUint = (AriesCoreUint) WorkSpace.SelectedContent;
            GraphCvAreaAtWorkSpace = AriesCoreUint.GraphArea;
            ToolKitManager.Instance.FreshGraphCvCoreAtWorkSpace(GraphCvAreaAtWorkSpace);
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
