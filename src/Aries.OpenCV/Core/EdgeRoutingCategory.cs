using System;
using System.Collections.Generic;

namespace Aries.OpenCV.Core
{
    public class EdgeRoutingCategory
    {
        public EdgeRoutingType EdgeRoutingType { set; get; }
        public string Header { set; get; }
        public string Icon { set; get; }

        private static readonly Dictionary<EdgeRoutingType, string> DictIcon =
            new Dictionary<EdgeRoutingType, string>()
            {
                [EdgeRoutingType.SimpleER] = "\ued74",
                [EdgeRoutingType.Bundling] = "\ued67",
                [EdgeRoutingType.PathFinder] = "\uef8c",
            };

        private static readonly Dictionary<EdgeRoutingType, string> DictHeader =
            new Dictionary<EdgeRoutingType, string>()
            {
                [EdgeRoutingType.SimpleER] = "Simple ER",
                [EdgeRoutingType.Bundling] = "Bundling",
                [EdgeRoutingType.PathFinder] = "Path Finder",
            };

        public static List<EdgeRoutingCategory> InitialEdgeRoutingCategories()
        {
            var categtories = new List<EdgeRoutingCategory>();
            foreach (var value in Enum.GetValues(typeof(EdgeRoutingType)))
            {
                var type = (EdgeRoutingType) value;
                categtories.Add(new EdgeRoutingCategory
                {
                    Header = DictHeader[type],
                    Icon = DictIcon[type],
                    EdgeRoutingType = type
                });
            }

            return categtories;
        }

    }

    public enum EdgeRoutingType
    {
        SimpleER,
        Bundling,
        PathFinder
    }
}