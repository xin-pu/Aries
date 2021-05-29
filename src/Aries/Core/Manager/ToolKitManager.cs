using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Aries.OpenCV.Core;

namespace Aries.Core
{
    public class ToolKitManager
    {

        public List<ToolKitGroup> ToolKitGroups { set; get; }

        private static readonly Lazy<ToolKitManager> lazy =
            new Lazy<ToolKitManager>(() => new ToolKitManager());

        public static ToolKitManager Instance => lazy.Value;


        public ToolKitManager()
        {
            ToolKitGroups = new List<ToolKitGroup>(0);
            Sync();
        }

        public void Sync()
        {
            ToolKitGroups.Clear();
            var types = BlockHelper.GetBlockClassType();
            var typeAll = types.Select(a => new ToolKitStruct
            {
                Name = a.Name,
                ClassType = a,
                BlockType = BlockHelper.GetBlockType(a),
                Icon = BlockHelper.GetBlockICon(a)
            }).ToList();


            typeAll.GroupBy(a => a.BlockType).ToList().ForEach(a =>
            {
                ToolKitGroups.Add(new ToolKitGroup
                {
                    GroupName = a.Key.ToString(),
                    ToolKitStructs = new ObservableCollection<ToolKitStruct>(a)
                });
            });
        }

        public void FreshGraphCvCoreAtWorkSpace(GraphCVArea graphCvCore)
        {
            ToolKitGroups.ForEach(a =>
            {
                a.ToolKitStructs.ToList().ForEach(b => { b.GraphCVAreaAtWorkSpace = graphCvCore; });
            });
        }

    }
}
