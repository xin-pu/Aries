using System.Diagnostics;
using System.Windows.Forms;
using GraphX.Common;

namespace AriesCV.Views
{
    public partial class CVWorkerItemView
    {



        
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
