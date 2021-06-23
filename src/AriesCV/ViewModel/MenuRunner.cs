using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace AriesCV.ViewModel
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

        private void ReloadGraphCommand_Execute()
        {
            Messenger.Default.Send(string.Empty, "ReloadGraphToken");
        }

        private void RunByGraphCommand_Execute()
        {
            Messenger.Default.Send(string.Empty, "RunGraphByDatasToken");
        }

        private void AutoSaveOutMatCommand_Execute()
        {
            Messenger.Default.Send(GraphCVRunConfig.AutoSaveOutMat, "SetEnableSaveImageToken");
        }
    }
}
