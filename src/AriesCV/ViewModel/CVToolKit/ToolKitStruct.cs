using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HandyControl.Controls;

namespace AriesCV.ViewModel.CVToolKit
{


    public class ToolKitStruct : ObservableObject
    {

        private bool _isVisiable = true;

        public string Name { set; get; }

        public string Category { set; get; }

        public string ICon { set; get; }

        public Type ClassType { set; get; }

        public bool IsVisiable
        {
            get { return _isVisiable; }
            set
            {
                _isVisiable = value;
                RaisePropertyChanged(() => IsVisiable);
            }
        }


        public RelayCommand CreateCvBlockCommand => new RelayCommand(
            CreateCvBlockCommand_Execute);

        private void CreateCvBlockCommand_Execute()
        {
            Growl.Info($"Closing");
        }
    }
}