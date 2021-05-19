using Aries.Utility;
using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Xml.Serialization;
using Aries.OpenCV.Core;
using Aries.Views;
using Microsoft.Win32;

namespace Aries.Core
{
    public class FileSystemManager
    {
        
        private static readonly Lazy<FileSystemManager> lazy =
            new Lazy<FileSystemManager>(() => new FileSystemManager());

        public static FileSystemManager Instance
        {
            get { return lazy.Value; }
        }

        private AriesManager ariesManager => AriesManager.Instance;

        private int ID = 0;
        public MainWindow MainWindow { set; get; }



        #region Command 

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

        public ICommand GraphCVSelectCommand
        {
            get { return new RelayCommand(GraphCVSelectCommand_Execute); }
        }

 

        private void GraphCVNewCommand_Execute()
        {
            ID++;

            var panel = new AriesCoreUint($"Default_{ID}");
            MainWindow.WorkSpace.Children.Clear();
            MainWindow.WorkSpace.Children.Add(panel);

            var graphCvCore = panel.GraphCvCore;
            ariesManager.GraphCvCores.Add(graphCvCore);
            ariesManager.GraphCvCore = graphCvCore;
        }


        private void GraphCVOpenCommand_Execute()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = @"aries(*.ar)|*.ar",
            };
            openFileDialog.ShowDialog();
            if (File.Exists(openFileDialog.FileName))
            {
                var graphCvCore = Open(openFileDialog.FileName);
                
                var panel = new AriesCoreUint(graphCvCore);
                MainWindow.WorkSpace.Children.Clear();
                MainWindow.WorkSpace.Children.Add(panel);

                ariesManager.GraphCvCores.Add(graphCvCore);
                ariesManager.GraphCvCore = graphCvCore;

                /// Add Code Later
                /// Import BlockEdges and BlockVertices to Area
            }
        }



        private bool GraphCVCloseSaveCommand_CanExecute()
        {
            return ariesManager.GraphCvCore != null;
        }

        private void GraphCVCloseCommand_Execute()
        {
            MainWindow.WorkSpace.Children.Clear();

            ariesManager.GraphCvCore.Dispose();
            ariesManager.GraphCvCores.Remove(ariesManager.GraphCvCore);

            var nextGraphCvCore = ariesManager.GraphCvCores.FirstOrDefault();
            if (nextGraphCvCore == null) return;
            ariesManager.GraphCvCore = nextGraphCvCore;

            var panel = new AriesCoreUint(nextGraphCvCore);
            MainWindow.WorkSpace.Children.Clear();
            MainWindow.WorkSpace.Children.Add(panel);
        }


        private bool GraphCVSaveCommand_CanExecute()
        {
            var fileName = ariesManager.GraphCvCore?.FileName;
            return fileName != null && File.Exists(fileName);
        }

        private void GraphCVSaveCommand_Execute()
        {
            Save(ariesManager.GraphCvCore.FileName, ariesManager.GraphCvCore);
        }

        private void GraphCVSaveAsCommand_Execute()
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = @"aries(*.ar)|*.ar",
            };
            var res = saveDialog.ShowDialog();
            if (res != true || saveDialog.FileName == "") return;
            Save(saveDialog.FileName,ariesManager.GraphCvCore);
        }

        private void GraphCVSelectCommand_Execute()
        {
            
        }

        #endregion




        public static GraphCVCore Open(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open);
            var formatter = new XmlSerializer(typeof(GraphCVCore),
                BlockHelper.GetBlockClassType().ToArray());
            var graphCvCore = (GraphCVCore)formatter.Deserialize(fs);
            graphCvCore.FileName = filename;
            return graphCvCore;
        }

        public void Save(string filename, GraphCVCore graphCvCore)
        {
            graphCvCore.BlockVertices = graphCvCore.GraphCvArea.VertexList?.Keys.ToList();
            graphCvCore.BlockEdges = graphCvCore.GraphCvArea.EdgesList?.Keys.ToList();
            using var fs = new FileStream(filename, FileMode.Create);
            var formatter = new XmlSerializer(typeof(GraphCVCore),
                BlockHelper.GetBlockClassType().ToArray());
            formatter.Serialize(fs, graphCvCore);
        }
    }
}
