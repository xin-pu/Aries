using System.Collections.ObjectModel;
using System.Linq;
using Aries.OpenCV;

namespace AriesCV.ViewModel.ToolKit
{

    public class ToolKitMatModel : ToolKitContainerModel
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public override void GenerateCVToolKitData()
        {
            CVToolKitData?.Clear();
            var types = BlockHelper.GetAllMatBlockCategory();

            var toolkit = types.Select(a => new ToolKitStruct
                {
                    Name = a.Key.Name,
                    ClassType = a.Key,
                    Category = a.Value,
                    ICon = BlockHelper.GetBlockICon(a.Value,"Mat"),
                    IsVisiable = true
                })
                .OrderBy(a => a.Category)
                .ThenBy(a => a.Name)
                .ToList();
            CVToolKitData = new ObservableCollection<ToolKitStruct>(toolkit);
        }

    }


}