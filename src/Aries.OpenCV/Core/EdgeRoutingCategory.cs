using System;
using System.Collections.Generic;
using GraphX.Common.Enums;

namespace Aries.OpenCV.Core
{
    public class EdgeRoutingCategory
    {
        public EdgeRoutingAlgorithmTypeEnum EdgeRoutingAlgorithmType { set; get; }
        public string Header { set; get; }
        public string Icon { set; get; }

        private static readonly Dictionary<EdgeRoutingAlgorithmTypeEnum, string> DictIcon =
            new Dictionary<EdgeRoutingAlgorithmTypeEnum, string>()
            {
                [EdgeRoutingAlgorithmTypeEnum.None] = "\ued74",
                [EdgeRoutingAlgorithmTypeEnum.SimpleER] = "\ued74",
                [EdgeRoutingAlgorithmTypeEnum.Bundling] = "\ued67",
                [EdgeRoutingAlgorithmTypeEnum.PathFinder] = "\uef8c",
            };

        private static readonly Dictionary<EdgeRoutingAlgorithmTypeEnum, string> DictHeader =
            new Dictionary<EdgeRoutingAlgorithmTypeEnum, string>()
            {
                [EdgeRoutingAlgorithmTypeEnum.None] = "NONE",
                [EdgeRoutingAlgorithmTypeEnum.SimpleER] = "Simple ER",
                [EdgeRoutingAlgorithmTypeEnum.Bundling] = "Bundling",
                [EdgeRoutingAlgorithmTypeEnum.PathFinder] = "Path Finder",
            };

        public static List<EdgeRoutingCategory> InitialEdgeRoutingCategories()
        {
            var categtories = new List<EdgeRoutingCategory>();
            foreach (var value in Enum.GetValues(typeof(EdgeRoutingAlgorithmTypeEnum)))
            {
                var type = (EdgeRoutingAlgorithmTypeEnum) value;
                categtories.Add(new EdgeRoutingCategory
                {
                    Header = DictHeader[type],
                    Icon = DictIcon[type],
                    EdgeRoutingAlgorithmType = type
                });
            }

            return categtories;
        }

    }
}