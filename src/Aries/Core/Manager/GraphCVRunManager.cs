using System;
using System.Collections.Generic;
using System.Windows.Input;
using Aries.OpenCV.GraphModel;
using Aries.Utility;
using GraphX.Common.Models;

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

        public ICommand RunGraphCVCommand
        {
            get { return new RelayCommand(RunGraphCVCommand_Execute); }
        }

        public ICommand StopGraphCVCommand
        {
            get { return new RelayCommand(StopGraphCVCommand_Execute); }
        }


        private void RunGraphCVCommand_Execute()
        {
            GraphSerializationDatas = AriesMain.GraphCvAreaAtWorkSpace.ExtractSerializationData();
            RunGraphCV();
        }

        private void StopGraphCVCommand_Execute()
        {

        }

        public void RunGraphCV()
        {
            foreach (var allVertexControl in AriesMain.GraphCvAreaAtWorkSpace.GetAllVertexControls())
            {
                var block = allVertexControl.GetDataVertex<BlockVertex>();
                try
                {
                    block.Execute();
                }
                catch
                {
                    // ignored
                }
            }
        }

    }
}
