using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Aries.OpenCV.BlockMat;
using Aries.OpenCV.GraphModel;
using Aries.OpenCV.GraphModel.Core;
using GalaSoft.MvvmLight.Command;
using GraphX.Common;
using GraphX.Controls;
using Microsoft.Win32;

namespace Aries.OpenCV.BlockMats
{
    public class AriesExpander : VertexMats
    {
        private List<VertexBasic> VertexDatas { set; get; }

        private List<BlockEdge> BlockEdges { set; get; }

        private List<ConnectionPointData> ConnectionPointDatas { set; get; }


        [Category("DATAIN")] public FileInfo[] MatFilesIn { set; get; }

        [Category("ARGUMENT")] public string GraphCVAriesFile { set; get; }


        [Category("COMMAND")]
        public RelayCommand SelectAriesCommand
        {
            get { return new RelayCommand(SelectAriesCommand_Execute); }
        }

        private void SelectAriesCommand_Execute()
        {
            var openFileDailog = new OpenFileDialog
            {
                Title = $"{ID}_{Name}",
                Filter = "ARIES文件|*.ar"
            };
            openFileDailog.ShowDialog();
            GraphCVAriesFile = openFileDailog.FileName;
        }


        public override bool CanCall()
        {
            var res = MatFilesIn != null && MatFilesIn.Length >= 1;
            return res;
        }

        public static object locker = new object();

        public override void Call()
        {
            MatFilesIn.ForEach(async mat =>
            {
                var (vertexBasics, edges, connectionPointDatas) = Clone();

                vertexBasics.ForEach(a => a.Reload());

                var import = vertexBasics
                    .FirstOrDefault(a => a.Name == "MatSource") as MatSource;
                if (import == null)
                    throw new ArgumentNullException();
                import.FileName = mat.FullName;

                GraphCVArea.ReloadWorkDirectory(vertexBasics, mat);

                await GraphCVArea.RunGraphByDataAsync(vertexBasics, edges, connectionPointDatas);

            });
        }

        private Tuple<List<VertexBasic>, List<BlockEdge>, List<ConnectionPointData>> Clone()
        {
            var datas = GraphCVFileStruct.DeserializeGraphDataFromFile(GraphCVAriesFile)
                .GraphSerializationDatas;
            VertexDatas = new List<VertexBasic>();
            BlockEdges = new List<BlockEdge>();
            ConnectionPointDatas = new List<ConnectionPointData>();


            foreach (var data in datas)
            {
                if (data.Data.GetType().IsSubclassOf(typeof(VertexBasic)))
                {
                    var a = data.Data as VertexBasic;
                    VertexDatas.Add(a);
                    continue;
                }

                if (data.Data.GetType() == typeof(ConnectionPointData))
                {
                    var a = data.Data as ConnectionPointData;
                    ConnectionPointDatas.Add(a);
                    continue;

                }

                if (data.Data.GetType() == typeof(BlockEdge))
                {
                    var a = data.Data as BlockEdge;
                    BlockEdges.Add(a);
                }
            }

            return new Tuple<List<VertexBasic>, List<BlockEdge>, List<ConnectionPointData>>(
                VertexDatas,
                BlockEdges,
                ConnectionPointDatas);
        }
    }
}
