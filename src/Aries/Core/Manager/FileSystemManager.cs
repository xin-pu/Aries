using Aries.Utility;
using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Xml.Serialization;
using Aries.OpenCV.Core;
using Aries.Views;
using HandyControl.Controls;
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

 

        private int ID = 0;
        public AriesMain AriesMain { set; get; }



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

        public ICommand GraphCVCloseAllCommand
        {
            get { return new RelayCommand(GraphCVCloseAllCommand_Execute, GraphCVCloseAllCommand_CanExecute); }
        }



        public ICommand GraphCVSaveCommand
        {
            get { return new RelayCommand(GraphCVSaveCommand_Execute, GraphCVSaveCommand_CanExecute); }
        }

        public ICommand GraphCVSaveAsCommand
        {
            get { return new RelayCommand(GraphCVSaveAsCommand_Execute, GraphCVCloseSaveCommand_CanExecute); }
        }



        private void GraphCVNewCommand_Execute()
        {
            ID++;
            var graphCvCore = new GraphCVCore($"Default_{ID}");
            var panel = new AriesCoreUint(graphCvCore);
            
            AddTapToTapControl(panel);
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

                AddTapToTapControl(panel);

                /// Add Code Later
                /// Import BlockEdges and BlockVertices to Area
            }
        }



        private bool GraphCVCloseSaveCommand_CanExecute()
        {
            return AriesMain.WorkSpace.SelectedItem != null;
        }

        private void GraphCVCloseCommand_Execute()
        {
            var selectItem = AriesMain.WorkSpace.SelectedItem;
            AriesMain.WorkSpace.Items.Remove(selectItem);
        }

        private bool GraphCVCloseAllCommand_CanExecute()
        {
            return true;
        }

        private void GraphCVCloseAllCommand_Execute()
        {
            AriesMain.WorkSpace.Items.Clear();
        }


        private bool GraphCVSaveCommand_CanExecute()
        {
            var selectContent = AriesMain.WorkSpace.SelectedContent;
            if (selectContent == null)
                return false;
            var ariesCoreUint = (AriesCoreUint) selectContent;
            var fileName = ariesCoreUint.GraphCvCore.FileName;
            return fileName != null && File.Exists(fileName);
        }

        private void GraphCVSaveCommand_Execute()
        {
            var selectContent = AriesMain.WorkSpace.SelectedContent;
            var ariesCoreUint = (AriesCoreUint)selectContent;
            Save(ariesCoreUint?.GraphCvCore.FileName, ariesCoreUint?.GraphCvCore);
        }

        private void GraphCVSaveAsCommand_Execute()
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = @"aries(*.ar)|*.ar",
            };
            var res = saveDialog.ShowDialog();
            if (res != true || saveDialog.FileName == "") return;
            var selectContent = AriesMain.WorkSpace.SelectedContent;
            var ariesCoreUint = (AriesCoreUint)selectContent;
            Save(saveDialog.FileName, ariesCoreUint.GraphCvCore);
        }

        private void AddTapToTapControl(AriesCoreUint ariesCoreUint)
        {
            var tabItem = new TabItem
            {
                Header = ariesCoreUint.GraphCvCore.Name,
                Content = ariesCoreUint,
            };
            AriesMain.WorkSpace.Items.Add(tabItem);
            AriesMain.WorkSpace.SelectedItem = tabItem;

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
