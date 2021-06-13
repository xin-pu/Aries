using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using Aries.OpenCV.Core;
using AriesCV.ViewModel.CVToolKit;
using GalaSoft.MvvmLight;

namespace AriesCV.ViewModel
{

    public class CVToolKitModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public CVToolKitModel()
        {
            GenerateCVToolKitData();
        }

        public string Title { set; get; } = "AriesCV";


        public ObservableCollection<ToolKitStruct> CVToolKitData { set; get; }

        private void GenerateCVToolKitData()
        {
            CVToolKitData = new ObservableCollection<ToolKitStruct>();
            var types = BlockHelper.GetAllCVCategory();

            var toolKitStructs = types.Select(a => new ToolKitStruct()
                {
                    Name = a.Key.Name,
                    ClassType = a.Key,
                    Category = a.Value,
                    ICon = BlockHelper.GetBlockICon(a.Value)
                })
                .ToList();
            CVToolKitData = new ObservableCollection<ToolKitStruct>(toolKitStructs);
        }

    }


}