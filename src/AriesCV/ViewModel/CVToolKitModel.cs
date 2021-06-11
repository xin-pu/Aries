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
            GenerateCVToolKitTreeData();
        }

        public string Title { set; get; } = "AriesCV";


        public ObservableCollection<ToolKitStruct> CVToolKitTreeData { set; get; }


        private void GenerateCVToolKitTreeData()
        {
            CVToolKitTreeData = new ObservableCollection<ToolKitStruct>();
            var types = BlockHelper.GetAllCVCategory();

            var toolKitStructs = types.Select(a => new ToolKitStruct
                {
                    Name = a.Key.Name,
                    ClassType = a.Key,
                    Catetogy = a.Value,
                    Children = new ObservableCollection<ToolKitStruct>(),
                    ICon = BlockHelper.GetBlockICon(a.Value)
                })
                .ToList();


            toolKitStructs.GroupBy(a => a.Catetogy)
                .OrderBy(a => a.Key)
                .ToList()
                .ForEach(a =>
                {
                    CVToolKitTreeData.Add(new ToolKitStruct
                    {
                        Name = a.Key,
                        ClassType = typeof(TreeViewItem),
                        Catetogy = a.Key,
                        Children = new ObservableCollection<ToolKitStruct>(a)
                    });
                });
        }

    }


}