using Aries.Utility;
using System;
using System.Linq;
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
            get { return new RelayCommand(GraphCVCloseCommand_Execute, GraphCVCloseSaveCommand_CanExecute); }
        }


        public ICommand GraphCVSaveCommand
        {
            get { return new RelayCommand(GraphCVSaveCommand_Execute, GraphCVCloseSaveCommand_CanExecute); }
        }

        public ICommand GraphCVSaveAsCommand
        {
            get { return new RelayCommand(GraphCVSaveAsCommand_Execute, GraphCVCloseSaveCommand_CanExecute); }
        }

        private int ID = 0;

        private static readonly Lazy<FileSystemManager> lazy =
            new Lazy<FileSystemManager>(() => new FileSystemManager());

        public static FileSystemManager Instance
        {
            get { return lazy.Value; }
        }

        private AriesManager ariesManager => AriesManager.Instance;

        private void GraphCVOpenCommand_Execute()
        {

        }

        private bool GraphCVCloseSaveCommand_CanExecute()
        {
            return ariesManager.LogicCoreCvSelect != null;
        }

        private void GraphCVCloseCommand_Execute()
        {
            ariesManager.LogicCoreCvs.Remove(ariesManager.LogicCoreCvSelect);
            ariesManager.LogicCoreCvSelect = ariesManager.LogicCoreCvs.FirstOrDefault();
        }

        private void GraphCVNewCommand_Execute()
        {
            ID++;
            var dgLogic = new LogicCoreCV($"Default_{ID}");
            ariesManager.LogicCoreCvs.Add(dgLogic);
            ariesManager.LogicCoreCvSelect = dgLogic;
        }

        private void GraphCVSaveCommand_Execute()
        {

        }

        private void GraphCVSaveAsCommand_Execute()
        {

        }


    }
}
