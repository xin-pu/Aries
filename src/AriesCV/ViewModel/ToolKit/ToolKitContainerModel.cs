﻿using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace AriesCV.ViewModel.ToolKit
{


    public abstract class ToolKitContainerModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        protected ToolKitContainerModel()
        {
            GenerateCVToolKitData();
            Messenger.Default.Register<string>(this, "ClearSearchKeyToken", ClearSearchkey);
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


        public abstract void GenerateCVToolKitData();


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


        private void ClearSearchkey(string searchKey)
        {
            SearchKey = string.Empty;
        }
    }
}
