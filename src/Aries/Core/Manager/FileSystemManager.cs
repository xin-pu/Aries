using Aries.Utility;
using System;
using System.IO;
using System.Windows.Input;
using Aries.Views;
using HandyControl.Controls;
using Microsoft.Win32;
using YAXLib;

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
            var name = $"Default_{ID}";
            var panel = new AriesCoreUint
            {
                GraphArea = {Name = name}
            };
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
                var graphCVFile = DeserializeGraphDataFromFile(openFileDialog.FileName);
                var fileInfo = new FileInfo(openFileDialog.FileName);

                var panel = new AriesCoreUint
                {
                    BackGroundManager = graphCVFile.BackGroundManager,
                    WaterMaskManager = graphCVFile.WaterMaskManager,
                    FileInfo = fileInfo,
                    WorkDirectory = fileInfo.DirectoryName
                };

                var grapArea = panel.GraphArea;
                grapArea.RebuildFromSerializationData(graphCVFile.GraphSerializationDatas);
                grapArea.SetVerticesDrag(true, true);
                grapArea.UpdateAllEdges();

                panel.ZoomControl.ZoomToFill();
                AddTapToTapControl(panel);

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
            try
            {
                var selectContent = AriesMain.WorkSpace.SelectedContent;
                var ariesCoreUint = (AriesCoreUint) selectContent;
                if (ariesCoreUint?.FileInfo == null)
                    return false;
                var fileName = ariesCoreUint.FileInfo.FullName;
                return File.Exists(fileName);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void GraphCVSaveCommand_Execute()
        {
            var selectContent = AriesMain.WorkSpace.SelectedContent;
            var ariesCoreUint = (AriesCoreUint) selectContent;
            SerializeGraphDataToFile(ariesCoreUint.FileInfo.FullName,
                new GraphCVFileStruct
                {
                    GraphSerializationDatas = ariesCoreUint.GraphArea.ExtractSerializationData(),
                    BackGroundManager = ariesCoreUint.BackGroundManager,
                    WaterMaskManager = ariesCoreUint.WaterMaskManager
                });
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
            var ariesCoreUint = (AriesCoreUint) selectContent;

            SerializeGraphDataToFile(saveDialog.FileName, new GraphCVFileStruct
            {
                GraphSerializationDatas = ariesCoreUint.GraphArea.ExtractSerializationData(),
                BackGroundManager = ariesCoreUint.BackGroundManager,
                WaterMaskManager = ariesCoreUint.WaterMaskManager
            });
            ariesCoreUint.FileInfo = new FileInfo(saveDialog.FileName);
        }

        private void AddTapToTapControl(AriesCoreUint ariesCoreUint)
        {
            var tabItem = new TabItem
            {
                Header = ariesCoreUint.GraphArea.Name,
                Content = ariesCoreUint,
            };
            AriesMain.WorkSpace.Items.Add(tabItem);
            AriesMain.WorkSpace.SelectedItem = tabItem;

        }

        #endregion



        #region Serialize DeSerialize

        /// <summary>
        /// Serializes data classes list to file
        /// </summary>
        /// <param name="filename">File name</param>
        /// <param name="modelsList">Data classes list</param>
        public static void SerializeGraphDataToFile(string filename, GraphCVFileStruct modelsList)
        {
            using var stream = File.Open(filename, FileMode.Create, FileAccess.Write, FileShare.Read);
            SerializeDataToStream(stream, modelsList);
        }

        /// <summary>
        /// Deserializes data classes list from file
        /// </summary>
        /// <param name="filename">File name</param>
        public static GraphCVFileStruct DeserializeGraphDataFromFile(string filename)
        {
            using var stream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            return DeserializeGraphDataFromStream(stream);
        }


        /// <summary>
        /// Serializes graph data list to a stream
        /// </summary>
        /// <param name="stream">The destination stream</param>
        /// <param name="modelsList">The graph data</param>
        public static void SerializeDataToStream(Stream stream, GraphCVFileStruct modelsList)
        {
            var serializer = new YAXSerializer(typeof(GraphCVFileStruct));
            using var textWriter = new StreamWriter(stream);
            serializer.Serialize(modelsList, textWriter);
            textWriter.Flush();
        }

        /// <summary>
        /// Deserializes graph data from a stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <returns>The graph data</returns>
        public static GraphCVFileStruct DeserializeGraphDataFromStream(Stream stream)
        {
            var deserializer = new YAXSerializer(typeof(GraphCVFileStruct));
            using var textReader = new StreamReader(stream);
            return (GraphCVFileStruct) deserializer.Deserialize(textReader);
        }

        #endregion

    }
}
