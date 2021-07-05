using System;
using System.Collections.ObjectModel;
using System.Linq;
using Aries.OpenCV;

namespace AriesCV.ViewModel.ToolKit
{
    public class ToolKitMatsModel : ToolKitContainerModel
    {
        public override void GenerateCVToolKitData()
        {
            CVToolKitData?.Clear();
            var types = BlockHelper.GetAllMatsBlockCategory();

            var toolkit = types.Select(a => new ToolKitStruct
                {
                    Name = a.Key.Name,
                    ClassType = a.Key,
                    Category = a.Value,
                    ICon = BlockHelper.GetBlockICon(a.Value,"Mats"),
                    IsVisiable = true
                })
                .OrderBy(a => a.Category)
                .ThenBy(a => a.Name)
                .ToList();
            CVToolKitData = new ObservableCollection<ToolKitStruct>(toolkit);
        }
    }
}
