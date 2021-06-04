﻿using System;
using System.Collections.Generic;

namespace Aries.OpenCV.Core
{
    
    public class LayOutCategtory
    {
        public LayoutType LayoutType { set; get; }
        public string Icon { set; get; }
        public string Header { set; get; }


        private static readonly Dictionary<LayoutType, string> DictIcon = new Dictionary<LayoutType, string>()
        {
            [LayoutType.TreeLeftToRight] = "\ued71",
            [LayoutType.TreeRightToLeft] = "\ued71",
            [LayoutType.TreeTopTpBottom] = "\uf0c7",
            [LayoutType.Circle] = "\ued67",
            [LayoutType.Cunstom] = "\ued71",
        };

        private static readonly Dictionary<LayoutType, string> DictHeader = new Dictionary<LayoutType, string>()
        {
            [LayoutType.TreeLeftToRight] = "从左到右树形布局",
            [LayoutType.TreeRightToLeft] = "从右到左树形布局",
            [LayoutType.TreeTopTpBottom] = "从上到下树形布局",
            [LayoutType.Circle] = "圆形布局",
            [LayoutType.Cunstom] = "自定义布局",
        };

        public static List<LayOutCategtory> InitiaLayOutCategtories()
        {
            var categtories = new List<LayOutCategtory>();
            foreach (var value in Enum.GetValues(typeof(LayoutType)))
            {
                var type = (LayoutType)value;
                categtories.Add(new LayOutCategtory
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
        Circle,
        Cunstom,
    }
}
