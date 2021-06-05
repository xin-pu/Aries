using System;
using System.Collections.Generic;
using GraphX.Common.Enums;

namespace Aries.OpenCV.Core
{
    
    public class LayoutCategory
    {
        public LayoutType LayoutType { set; get; }
        public EdgeRoutingAlgorithmTypeEnum EdgeRoutingType { set; get; }
        public string Icon { set; get; }
        public string Header { set; get; }


        private static readonly Dictionary<LayoutType, string> DictIcon = new Dictionary<LayoutType, string>()
        {
            [LayoutType.TreeLeftToRight] = "\uf118",
            [LayoutType.TreeRightToLeft] = "\uf119",
            [LayoutType.TreeTopTpBottom] = "\ue69c",
            [LayoutType.Circular] = "\ue732",
            [LayoutType.Custom] = "\ue743",
        };

        private static readonly Dictionary<LayoutType, string> DictHeader = new Dictionary<LayoutType, string>()
        {
            [LayoutType.TreeLeftToRight] = "从左到右树形布局",
            [LayoutType.TreeRightToLeft] = "从右到左树形布局",
            [LayoutType.TreeTopTpBottom] = "从上到下树形布局",
            [LayoutType.Circular] = "圆形布局",
            [LayoutType.Custom] = "自定义布局",
        };

        public static List<LayoutCategory> InitiaLayOutCategories()
        {
            var categtories = new List<LayoutCategory>();
            foreach (var value in Enum.GetValues(typeof(LayoutType)))
            {
                var type = (LayoutType)value;
                categtories.Add(new LayoutCategory
                {
                    Header = DictHeader[type],
                    Icon = DictIcon[type],
                    LayoutType = type
                });
            }

            return categtories;
        }
    }


    public enum LayoutType
    {
        TreeLeftToRight,
        TreeTopTpBottom,
        TreeRightToLeft,
        Circular,
        Custom,
    }


 
}
