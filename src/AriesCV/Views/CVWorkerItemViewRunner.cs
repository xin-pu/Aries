using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Aries.OpenCV.GraphModel;
using System.Windows.Forms;
using GraphX.Common;
using MessageBox = HandyControl.Controls.MessageBox;

namespace AriesCV.Views
{
    public partial class CVWorkerItemView
    {

        public async Task ReloadAllBlockAsync()
        {
            var tasks = VertexControls.Keys.Select(vc => Task.Run(vc.Reload));
            await Task.WhenAll(tasks);
        }

        public async Task SetEnableSaveImageAsync(bool isAutoSave)
        {
            var tasks = VertexControls.Keys
                .OfType<VertexMat>()
                .Select(vc => { return Task.Run(() => vc.EnableSaveMat = isAutoSave); });
            await Task.WhenAll(tasks);
        }

        public async Task RunGraphByDataAsync()
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


        public void OpenWorkDirectory()
        {
            Process.Start("explorer.exe", GraphCvRunConfig.WorkDirectory);
        }

        public void ChangeWorkDirectory()
        {
            var openFileDialog = new FolderBrowserDialog
            {
                SelectedPath = GraphCvRunConfig.WorkDirectory
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var dire = openFileDialog.SelectedPath;
                GraphCvRunConfig.WorkDirectory = dire;
                VertexControls.ForEach(a => a.Key.WorkDirectory = dire);
            }
        }


    }
}
