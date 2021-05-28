using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Aries.OpenCV.GraphModel;
using Aries.Utility;
using GraphX.Common.Models;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Aries.Core
{
    public class GraphCVRunManager
    {
        private static readonly Lazy<GraphCVRunManager> lazy =
            new Lazy<GraphCVRunManager>(() => new GraphCVRunManager());

        public static GraphCVRunManager Instance
        {
            get { return lazy.Value; }
        }


        public AriesMain AriesMain { set; get; }

        public List<GraphSerializationData> GraphSerializationDatas { set; get; }

        public ICommand StepRunGraphCVCommand
        {
            get { return new RelayCommand(StepRunGraphCVCommand_Execute); }
        }

        public ICommand RunGraphCVCommand
        {
            get { return new RelayCommand(RunGraphCVCommand_Execute); }
        }

        public ICommand StopGraphCVCommand
        {
            get { return new RelayCommand(StopGraphCVCommand_Execute); }
        }


        private void StepRunGraphCVCommand_Execute()
        {

        }

        private void RunGraphCVCommand_Execute()
        {
            try
            {
                RunGraphCV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void StopGraphCVCommand_Execute()
        {

        }

        public async void RunGraphCV()
        {
            await Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                var vertexs = AriesMain.GraphCvAreaAtWorkSpace.VertexList;
                var edges = AriesMain.GraphCvAreaAtWorkSpace.EdgesList;


                /// Prepare Run
                var verDatas = vertexs.Select(a => a.Key).ToList();
                verDatas.ForEach(vertex => vertex.Reload());

                while (verDatas.Any(a => a.CanExecute() && a.Status == BlockStatus.ToRun))
                {
                    /// Get Vertext CanRun
                    var vertexsRun = verDatas
                        .Where(a => a.CanExecute() &&
                                    a.Status == BlockStatus.ToRun)
                        .ToList();

                    /// Run Execute
                    var ExecuteTask = vertexsRun.Select(a => Task.Run(a.ExecuteCommand_Execute));
                    await Task.WhenAll(ExecuteTask);

                    /// Check Run Status
                    if (vertexsRun.Any(a => a.Status == BlockStatus.Exception))
                        break;

                    /// Update From Source's OutPut to Target's Input
                    vertexsRun.ForEach(vertexRun =>
                    {
                        var edgesFromRun = edges.Keys
                            .Where(a => a.Source == vertexRun)
                            .ToList();


                        edgesFromRun.ForEach(edgeActive =>
                        {
                            var source = edgeActive.Source;
                            var sourceHeaderName = vertexs[source].VertexConnectionPointsList
                                .FirstOrDefault(a => a.Id == edgeActive.SourceConnectionPointId)?.Header;


                            var target = edgeActive.Target;
                            var targetHeaderName = vertexs[target].VertexConnectionPointsList
                                .FirstOrDefault(a => a.Id == edgeActive.TargetConnectionPointId)?.Header;

                            var data = source.GetProperty(sourceHeaderName);
                            target.SetProperty(targetHeaderName, data);


                        });
                    });

                }

            });
        }

    }
}
