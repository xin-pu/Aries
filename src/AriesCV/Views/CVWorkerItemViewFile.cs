using System;
using System.IO;
using Aries.OpenCV.GraphModel;
using GraphX.Common;
using GraphX.Common.Enums;
using HandyControl.Controls;
using Microsoft.Win32;

namespace AriesCV.Views
{
    public partial class CVWorkerItemView
    {

        public GraphCVFileStruct GetCvFileStruct()
        {
            GraphCVArea.VertexList.Keys.ForEach(a => a.Reload());
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

            var graphCVFile = GraphCVFileStruct.DeserializeGraphDataFromFile(openFileDialog.FileName);
            return new CVWorkerItemView(graphCVFile);
        }

        public void SaveToSelf()
        {
            if (FileInfo == null)
            {
                SaveToAriesFile();
            }
            GraphCVFileStruct.SerializeGraphDataToFile(FileInfo.FullName, GetCvFileStruct());
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
            GraphCVFileStruct.SerializeGraphDataToFile(saveDialog.FileName, GetCvFileStruct());
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


      
    }
}