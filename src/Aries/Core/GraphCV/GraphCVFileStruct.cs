using System;
using System.Collections.Generic;
using GraphX.Common.Models;

namespace Aries.Core
{
    [Serializable]
    public class GraphCVFileStruct 
    {

        public List<GraphSerializationData> GraphSerializationDatas { set; get; }

        public WaterMaskManager WaterMaskManager { set; get; }

        public BackGroundManager BackGroundManager { set; get; }



    }



}
