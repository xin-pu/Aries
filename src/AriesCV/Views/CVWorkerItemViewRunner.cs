using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aries.OpenCV.GraphModel;
using GraphX.Controls;
using HandyControl.Controls;

namespace AriesCV.Views
{
    public partial class CVWorkerItemView
    {

  



        public async Task ReloadAllBlock()
        {
            var tasks = VertexControls.Keys.Select(vc => Task.Run(vc.Reload));
            await Task.WhenAll(tasks);
        }

        public async Task SetEnableSaveImage(bool isAutoSave)
        {
            var tasks = VertexControls.Keys.Select(vc => { return Task.Run(() => vc.EnableSaveMat = isAutoSave); });
            await Task.WhenAll(tasks);
        }

        public async Task RunGraphByDatas()
        {
            try
            {

                var verDatas = VertexControls.Select(a => a.Key).ToList();
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


                    /// Check Run Status
                    if (vertexsRun.Any(a => a.Status == BlockStatus.Exception))
                        break;

                    /// Update From Source's OutPut to Target's Input
                    vertexsRun.ForEach(vertexRun =>
                    {
                        var edgesFromRun = EdgeControls.Keys
                            .Where(a => a.Source == vertexRun)
                            .ToList();


                        foreach (var edgeActive in edgesFromRun)
                        {
                            var source = edgeActive.Source;
                            var sourcePoint = VertexControls[source].VertexConnectionPointsList
                                .FirstOrDefault(a => a.Id == edgeActive.SourceConnectionPointId);
                            var sourceHeaderName = sourcePoint?.Header;
                            if (sourcePoint == null)
                                continue;

                            var target = edgeActive.Target;
                            var targetPoint = VertexControls[target].VertexConnectionPointsList
                                .FirstOrDefault(a => a.Id == edgeActive.TargetConnectionPointId);
                            var targetHeaderName = targetPoint?.Header;
                            if (targetPoint == null)
                                continue;

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
                        }
                    });

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }




    }
}
