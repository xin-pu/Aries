using System;
using System.Windows.Input;
using Aries.OpenCV.GraphModel;
using Aries.Utility;
using GraphX.Logic.Models;
using QuickGraph;

namespace Aries.Core
{
 
    public class LogicCoreCV :
        GXLogicCore<BlockVertex, BlockEdge, BidirectionalGraph<BlockVertex, BlockEdge>>
    {

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
            throw new NotImplementedException();
        }

        private void StopGraphCVCommand_Execute()
        {
            throw new NotImplementedException();
        }
    }
}