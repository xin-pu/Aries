using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Aries.OpenCV.BlockMat;
using Aries.OpenCV.GraphModel;
using Aries.OpenCV.GraphModel.Core;
using GalaSoft.MvvmLight.Command;
using GraphX.Common;
using GraphX.Controls;
using Microsoft.Win32;
using OpenCvSharp;

namespace Aries.OpenCV.BlockMats
{
    public class AriesExpander : VertexMats
    {
        private List<VertexBasic> VertexDatas { set; get; }

        private List<BlockEdge> BlockEdges { set; get; }

        private List<ConnectionPointData> ConnectionPointDatas { set; get; }


        [Category("DATAIN")] public string[] MatsIn { set; get; }

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

        }


        public override bool CanCall()
        {
            var res = MatsIn != null && MatsIn.Length >= 1;
            return res;
        }

        public override void Call()
        {
            MatsIn.ForEach(async mat =>
            {
                VertexDatas.ForEach(a => a.Reload());

                var import = VertexDatas.FirstOrDefault(a => a.Name == "MatSource") as MatSource;
                import.FileName = mat;


                await GraphCVArea.RunGraphByDataAsync(VertexDatas, BlockEdges, ConnectionPointDatas);


            });
        }
    }
}
