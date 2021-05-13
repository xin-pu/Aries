using Aries.Utility;
using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Aries.OpenCV.Blocks.Processing;
using GraphX.Controls;
using Microsoft.Win32;

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
            get { return new RelayCommand(GraphCVSaveCommand_Execute, GraphCVSaveCommand_CanExecute); }
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
            var openFileDialog = new OpenFileDialog
            {
                Filter = @"aries(*.ar)|*.ar",
            };
            openFileDialog.ShowDialog();
            if (File.Exists(openFileDialog.FileName))
            {
                var graphCvCore = GraphCVCore.Open(openFileDialog.FileName);
                ariesManager.GraphCvCores.Add(graphCvCore);
                ariesManager.GraphCvCore = graphCvCore;

                /// Add Code Later
                /// Import BlockEdges and BlockVertices to Area
            }
        }

        public MainWindow MainWindow { set; get; }

        private bool GraphCVCloseSaveCommand_CanExecute()
        {
            return ariesManager.GraphCvCore != null;
        }

        private void GraphCVCloseCommand_Execute()
        {
            ariesManager.GraphCvCores.Remove(ariesManager.GraphCvCore);
            ariesManager.GraphCvCore = ariesManager.GraphCvCores.FirstOrDefault();
        }

        private void GraphCVNewCommand_Execute()
        {
            ID++;
            var area = MainWindow.dg_Area;
            area.Children.Clear();
            var dgLogic = new GraphCVCore($"Default_{ID}", area);
            ariesManager.GraphCvCores.Add(dgLogic);
            ariesManager.GraphCvCore = dgLogic;
        }

        private bool GraphCVSaveCommand_CanExecute()
        {
            var fileName = ariesManager.GraphCvCore?.FileName;
            return fileName != null && File.Exists(fileName);
        }

        private void GraphCVSaveCommand_Execute()
        {
            ariesManager.GraphCvCore.Save(ariesManager.GraphCvCore.FileName);
        }

        private void GraphCVSaveAsCommand_Execute()
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = @"aries(*.ar)|*.ar",
            };
            var res = saveDialog.ShowDialog();
            if (res != true || saveDialog.FileName == "") return;
            ariesManager.GraphCvCore.Save(saveDialog.FileName);
        }


    }
}
