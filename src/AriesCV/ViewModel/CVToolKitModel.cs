using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Aries.OpenCV.Core;
using AriesCV.ViewModel.CVToolKit;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

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

        
        private string _searchKey;
        private ObservableCollection<ToolKitStruct> _cVToolKitData;


        public string SearchKey
        {
            get { return _searchKey; }
            set
            {
                _searchKey = value;
                RaisePropertyChanged(() => SearchKey);
            }
        }


        public ObservableCollection<ToolKitStruct> CVToolKitData
        {
            get { return _cVToolKitData; }
            set
            {
                _cVToolKitData = value;
                RaisePropertyChanged(() => CVToolKitData);
            }
        }


        private void GenerateCVToolKitData()
        {
            CVToolKitData?.Clear();
            var types = BlockHelper.GetAllCVCategory();

            var toolkit = types.Select(a => new ToolKitStruct
                {
                    Name = a.Key.Name,
                    ClassType = a.Key,
                    Category = a.Value,
                    ICon = BlockHelper.GetBlockICon(a.Value),
                    IsVisiable = true
                })
                .ToList();
            CVToolKitData = new ObservableCollection<ToolKitStruct>(toolkit);
        }


        public RelayCommand OnSearchCommand
        {
            get => new RelayCommand(OnSearchCommand_Execute);
        }


        private void OnSearchCommand_Execute()
        {
            if (string.IsNullOrEmpty(SearchKey))
            {
                CVToolKitData.ToList().ForEach(a => a.IsVisiable = true);
            }
            else
            {
                CVToolKitData.ToList().ForEach(item =>
                {
                    item.IsVisiable = item.Name.ToUpper().Contains(SearchKey.ToUpper()) ||
                                      item.Category.ToUpper().Contains(SearchKey.ToUpper());
                });
            }
        }

        public List<ToolKitStruct> CVToolKitFilter { set; get; }
    }


}