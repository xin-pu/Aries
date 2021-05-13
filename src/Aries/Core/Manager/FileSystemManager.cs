using Aries.Utility;
using System;
using System.Linq;
using System.Windows.Input;
using Aries.OpenCV.Blocks.Import;
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
            var area = MainWindow.Instance.dg_Area;
            area.Children.Clear();
            var dgLogic = new GraphCVManager($"Default_{ID}", area);
            ariesManager.LogicCoreCvs.Add(dgLogic);
            ariesManager.LogicCoreCvSelect = dgLogic;

           
            var a = new Blur();
            area.AddVertexAndData(a, new VertexControl(a));

            if (area.VertexList.Count == 1)
            {
                area.VertexList.First().Value.SetPosition(0, 0);
                area.UpdateLayout(); //update layout to update vertex size
            }
            else area.RelayoutGraph(true);
        }

        private void GraphCVSaveCommand_Execute()
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = @"aries(*.ar)|*.ar",
            };
            var res = saveDialog.ShowDialog();
            if (res != true || saveDialog.FileName == "") return;
            ariesManager.LogicCoreCvSelect.Save(saveDialog.FileName);
        }

        private void GraphCVSaveAsCommand_Execute()
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = @"aries(*.ar)|*.ar",
            };
            var res = saveDialog.ShowDialog();
            if (res != true || saveDialog.FileName == "") return;
            ariesManager.LogicCoreCvSelect.Save(saveDialog.FileName);
        }


    }
}
