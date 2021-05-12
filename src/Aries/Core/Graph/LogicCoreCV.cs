using System;
using Aries.OpenCV.GraphModel;
using GraphX.Logic.Models;
using QuickGraph;

namespace Aries.Core
{
    public class LogicCoreCV :
        GXLogicCore<BlockVertex, BlockEdge, BidirectionalGraph<BlockVertex, BlockEdge>>
    {


        public LogicCoreCV()
        {
            WaterMaskManager = new WaterMaskManager();
            BackGroundManager = new BackGroundManager();
        }

        public WaterMaskManager WaterMaskManager { set; get; }
        public BackGroundManager BackGroundManager { set; get; }

        public string Name { set; get; }
        public DateTime CreateTime { set; get; }
        public DateTime LastUpdateTime { set; get; }


    }
}