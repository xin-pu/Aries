using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace AriesCV.ViewModel.Menu
{
    public class MenuRunner : ViewModelBase
    {

        private GraphCVRunConfig _graphCVRunConfig;

        public GraphCVRunConfig GraphCVRunConfig
        {
            get { return _graphCVRunConfig; }
            set
            {
                _graphCVRunConfig = value;
                RaisePropertyChanged(() => GraphCVRunConfig);
            }
        }

        public RelayCommand RunByGraphCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(RunByGraphCommand_Execute)).Value;

        public RelayCommand ReloadGraphCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(ReloadGraphCommand_Execute)).Value;

        public RelayCommand AutoSaveOutMatCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(AutoSaveOutMatCommand_Execute)).Value;

        public RelayCommand ChangeWorkDirectoryCommand=>
            new Lazy<RelayCommand>(() => new RelayCommand(ChangeWorkDirectoryCommand_Execute)).Value;

        public RelayCommand OpenWorkDirectoryCommand =>
            new Lazy<RelayCommand>(() => new RelayCommand(OpenWorkDirectoryCommand_Execute)).Value;

        
        private void ReloadGraphCommand_Execute()
        {
            Messenger.Default.Send(string.Empty, "ReloadGraphToken");
        }

        private void RunByGraphCommand_Execute()
        {
            Messenger.Default.Send(string.Empty, "RunGraphByDataToken");
        }

        private void AutoSaveOutMatCommand_Execute()
        {
            Messenger.Default.Send(GraphCVRunConfig.AutoSaveOutMat, "SetEnableSaveImageToken");
        }

        private void OpenWorkDirectoryCommand_Execute()
        {
            Messenger.Default.Send(GraphCVRunConfig.WorkDirectory, "OpenWorkDirectoryToken");
        }

        private void ChangeWorkDirectoryCommand_Execute()
        {
            Messenger.Default.Send(GraphCVRunConfig.WorkDirectory, "ChangeWorkDirectoryToken");
        }
    }
}
