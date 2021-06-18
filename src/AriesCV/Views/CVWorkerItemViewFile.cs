using System;
using System.IO;
using AriesCV.ViewModel;
using GraphX.Common.Enums;
using HandyControl.Controls;
using Microsoft.Win32;
using YAXLib;

namespace AriesCV.Views
{
    public partial class CVWorkerItemView
    {

        public GraphCVFileStruct GetCvFileStruct()
        {
            return new GraphCVFileStruct
            {
                Name = Name,
                GraphSerializationDatas = GraphCVArea.ExtractSerializationData(),
                GraphCvLayoutConfig = GraphCvLayoutConfig
            };
        }


        public static CVWorkerItemView OpenFromAriesFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = @"aries(*.ar)|*.ar",
            };
            var res = openFileDialog.ShowDialog();
            if (res != true || openFileDialog.FileName == "")
                return null;

            var graphCVFile = DeserializeGraphDataFromFile(openFileDialog.FileName);
            return new CVWorkerItemView(graphCVFile);
        }

        public void SaveToSelf()
        {
            if (FileInfo == null)
            {
                SaveToAriesFile();
            }
            SerializeGraphDataToFile(FileInfo.FullName, GetCvFileStruct());
        }

        public void SaveToAriesFile()
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = @"aries(*.ar)|*.ar",
            };
            var res = saveDialog.ShowDialog();
            if (res != true || saveDialog.FileName == "") return;
            try
            {
                Name = Path.GetFileNameWithoutExtension(saveDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            SerializeGraphDataToFile(saveDialog.FileName, GetCvFileStruct());
            FileInfo = new FileInfo(saveDialog.FileName);
        }

        public void SaveToPicture()
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = @"PNG(*.png)|*.png",
            };
            var res = saveDialog.ShowDialog();
            if (res != true || saveDialog.FileName == "") return;

            GraphCVArea.ExportAsImage(saveDialog.FileName, ImageType.PNG);
        }


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
            var res = DeserializeGraphDataFromStream(stream);
            return res;
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
            return (GraphCVFileStruct)deserializer.Deserialize(textReader);
        }

        #endregion
    }
}