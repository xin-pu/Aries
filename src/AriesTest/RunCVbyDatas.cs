using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aries.OpenCV.GraphModel;
using AriesCV.Views;
using GraphX.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AriesTest
{
    [TestClass]
    public class RunCVbyDatas
    {

        public List<VertexBasic> verDatas { set; get; }
        public List<BlockEdge> Edges { set; get; }

        public List<ConnectionPointData> ConnectionPoints { set; get; }

        [TestMethod]
        public async Task TestMethod1()
        {
            var graphCVFile = CVWorkerItemView.DeserializeGraphDataFromFile(@"E:\Projects CV\GetSerialNumber.ar");
            var datas = graphCVFile.GraphSerializationDatas;
            verDatas = new List<VertexBasic>();
            Edges = new List<BlockEdge>();
            ConnectionPoints=new List<ConnectionPointData>();

            foreach (var data in datas)
            {
                if (data.Data.GetType().IsSubclassOf(typeof(VertexBasic)))
                {
                    var a = data.Data as VertexBasic;
                    verDatas.Add(a);
                    continue;
                }

                if (data.Data.GetType()==typeof(ConnectionPointData))
                {
                    var a = data.Data as ConnectionPointData;
                    ConnectionPoints.Add(a);
                    continue;

                }

                if (data.Data.GetType() == typeof(BlockEdge))
                {
                    var a = data.Data as BlockEdge;
                    Edges.Add(a);
                }
            }

            verDatas.ForEach(a =>
            {
                a.Reload();
                a.WorkDirectory = "D:\\";
            });

            while (verDatas.Any(a => a.CanExecute() && a.Status == BlockStatus.ToRun))
            {
                /// Get Vertext CanRun
                var vertexsRun = verDatas
                    .Where(a => a.CanExecute() &&
                                a.Status == BlockStatus.ToRun)
                    .ToList();

                /// Run Core Execute
                var executeTask = vertexsRun.Select(a => Task.Run(a.ExecuteCommand_Execute));
                await Task.WhenAll(executeTask);


                /// Check Run Status
                if (vertexsRun.Any(a => a.Status == BlockStatus.Exception))
                    break;

                /// Update From Source's OutPut to Target's Input
                vertexsRun.ForEach(vertexRun =>
                {
                    var edgesFromRun = Edges
                        .Where(a => a.Source.ID == vertexRun.ID)
                        .ToList();


                    foreach (var edgeActive in edgesFromRun)
                    {

                        var sourceAtEdge = edgeActive.Source;
                        var targetAtEdge = edgeActive.Target;

                        var sourceReal = verDatas.FirstOrDefault(a => a.ID == sourceAtEdge.ID);
                        var targetReal = verDatas.FirstOrDefault(a => a.ID == targetAtEdge.ID);

                        if (sourceReal == null || targetReal == null)
                            break;

                        var sourceID = sourceReal.ID;
                        var targetID = targetReal.ID;

                        /// 从所有连接点中找到唯一目标
                        var sourcePoint = ConnectionPoints.FirstOrDefault(a =>
                            a.ParentID == sourceID &&
                            a.ID== edgeActive.SourceConnectionPointId);
                        var targetPoint = ConnectionPoints.FirstOrDefault(a =>
                            a.ParentID == targetID &&
                            a.ID == edgeActive.TargetConnectionPointId);
                        if (sourcePoint == null || targetPoint == null)
                            break;

                        var sourceHeaderName = sourcePoint.Header;
                        var targetHeaderName = targetPoint.Header;

                        if (targetPoint.TypeFullName == sourcePoint.TypeFullName)
                        {
                            var obj = sourceReal.GetProperty(sourceHeaderName);
                            targetReal.SetProperty(targetHeaderName, obj);
                        }
                        else
                        {
                            var mat = sourceReal.GetPropertyAsMat(sourceHeaderName);
                            targetReal.SetPropertyAsMat(targetHeaderName, mat);
                        }

                    }
                });

            }
        }


      
    }
}
