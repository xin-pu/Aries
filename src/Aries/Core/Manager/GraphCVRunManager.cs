using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Aries.OpenCV.GraphModel;
using Aries.Utility;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Aries.Core
{
    public class GraphCVRunManager
    {

        private GraphCVArea GraphCvArea { get; }

        public string WorkDirectory { set; get; } =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Saved Pictures");
        public Action<List<MatRecord>> AppendMatRecordAction;
        public Action ClearMatRecordsAction;

        public GraphCVRunManager(GraphCVArea graphCvArea)
        {
            GraphCvArea = graphCvArea;
        }

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

        public ICommand OpenWorkDirectorCommand
        {
            get { return new RelayCommand(OpenWorkDirectorCommand_Execute); }
        }

       

        public ICommand ChangeWorkDirectorCommand
        {
            get { return new RelayCommand(ChangeWorkDirectorCommand_Execute); }
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
        }

        private void StopGraphCVCommand_Execute()
        {

        }

        public async void RunGraphCV()
        {
            await Application.Current.Dispatcher.InvokeAsync(async () =>
            {

                try
                {
                    var vertexs = GraphCvArea.VertexList;
                    var edges = GraphCvArea.EdgesList;


                    /// Prepare Run
                    ClearMatRecordsAction?.Invoke();
                    var verDatas = vertexs.Select(a => a.Key).ToList();
                    verDatas.ForEach(vertex => vertex.Reload());


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

                        /// Run SaveBlockImage
                        var saveBlockTask = vertexsRun.Select(a => Task.Run(() =>
                        {
                            var imageRecord = a.SaveBlock(WorkDirectory);
                            AppendMatRecordAction?.Invoke(imageRecord);
                        }));
                        await Task.WhenAll(saveBlockTask);

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
                                var sourcePoint = vertexs[source].VertexConnectionPointsList
                                    .FirstOrDefault(a => a.Id == edgeActive.SourceConnectionPointId);
                                var sourceHeaderName = sourcePoint?.Header;


                                var target = edgeActive.Target;
                                var targetPoint = vertexs[target].VertexConnectionPointsList
                                    .FirstOrDefault(a => a.Id == edgeActive.TargetConnectionPointId);
                                var targetHeaderName = targetPoint?.Header;


                                if (targetPoint.TypeFullName == sourcePoint.TypeFullName)
                                {
                                    var obj = source.GetProperty(sourceHeaderName);
                                    target.SetProperty(targetHeaderName, obj);
                                }
                                else
                                {
                                    var mat = source.GetPropertyAsMat(sourceHeaderName);
                                    target.SetPropertyAsMat(targetHeaderName, mat);
                                }
                            });
                        });

                    }
                }
                catch (Exception ex)
                {
                    ;
                }

            });
        }


        private void OpenWorkDirectorCommand_Execute()
        {
            throw new NotImplementedException();
        }
        private void ChangeWorkDirectorCommand_Execute()
        {
            throw new NotImplementedException();
        }
    }
}
