using Aries.Utility;
using System;
using System.Windows.Input;

namespace Aries.Core
{
    public class FileSystemManager
    {

        public ICommand GraphCVNewCommand
        {
            get { return new RelayCommand(GraphCVNewCommand_Execute); }
        }

        public ICommand GraphCVOpenCommand
        {
            get { return new RelayCommand(GraphCVOpenCommand_Execute); }
        }

        public ICommand GraphCVCloseCommand
        {
            get { return new RelayCommand(GraphCVCloseCommand_Execute); }
        }

        
        public ICommand GraphCVSaveCommand
        {
            get { return new RelayCommand(GraphCVSaveCommand_Execute); }
        }

        public ICommand GraphCVSaveAsCommand
        {
            get { return new RelayCommand(GraphCVSaveAsCommand_Execute); }
        }

        private static readonly Lazy<FileSystemManager> lazy =
            new Lazy<FileSystemManager>(() => new FileSystemManager());

        public static FileSystemManager Instance
        {
            get { return lazy.Value; }
        }

        private void GraphCVOpenCommand_Execute()
        {
           
        }

        private void GraphCVCloseCommand_Execute()
        {
            AriesManager.Instance.LogicCoreCvs.Remove(AriesManager.Instance.LogicCoreCvSelect);
        }

        private void GraphCVNewCommand_Execute()
        {
            var dgLogic = new LogicCoreCV();
            AriesManager.Instance.LogicCoreCvs.Add(dgLogic);
            AriesManager.Instance.LogicCoreCvSelect = dgLogic;
        }

        private void GraphCVSaveCommand_Execute()
        {
           
        }

        private void GraphCVSaveAsCommand_Execute()
        {
            
        }


    }
}
