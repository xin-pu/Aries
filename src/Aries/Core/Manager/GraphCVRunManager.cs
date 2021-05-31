﻿using System;
using System.Collections.Generic;
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

        public string WorkDirectory { set; get; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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
