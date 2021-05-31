using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Aries.OpenCV.Core;
using Aries.Utility;

namespace Aries.Core
{
    public class ToolKitManager : INotifyPropertyChanged
    {

        private ObservableCollection<ToolKitGroup> _toolKitGroups;

        public ObservableCollection<ToolKitGroup> ToolKitGroups
        {
            set { UpdateProperty(ref _toolKitGroups, value); }
            get { return _toolKitGroups; }
        }

        public List<ToolKitStruct> ToolKitStructs { set; get; }

        private static readonly Lazy<ToolKitManager> lazy =
            new Lazy<ToolKitManager>(() => new ToolKitManager());

        public static ToolKitManager Instance => lazy.Value;


        public ToolKitManager()
        {
            ToolKitGroups = new ObservableCollection<ToolKitGroup>();
            Sync();
        }

        public void Sync()
        {
            var types = BlockHelper.GetAllCVCategory();
            ToolKitStructs = types.Select(a => new ToolKitStruct
                {
                    Name = a.Key.Name,
                    BlockType = BlockHelper.GetBlockType(a.Key).ToString(),
                    ClassType = a.Key,
                    CatetogyType = a.Value
                })
                .OrderBy(a => a.ClassType.Name)
                .ToList();
            GroupByCvCategotyCommand_Execute();
        }

        public void FreshGraphCvCoreAtWorkSpace(GraphCVArea graphCvCore)
        {
            ToolKitGroups.ToList().ForEach(a =>
            {
                a.ToolKitStructs.ToList().ForEach(b => { b.GraphCVAreaAtWorkSpace = graphCvCore; });
            });
        }

        #region  Command



        public ICommand GroupByCvCategotyCommand
        {
            get { return new RelayCommand(GroupByCvCategotyCommand_Execute); }
        }

        private void GroupByCvCategotyCommand_Execute()
        {
            ToolKitGroups.Clear();


            ToolKitStructs.GroupBy(a => a.CatetogyType)
                .OrderBy(a => a.Key)
                .ToList()
                .ForEach(a =>
                {
                    ToolKitGroups.Add(new ToolKitGroup
                    {
                        GroupName = a.Key.ToString(),
                        ToolKitStructs = new ObservableCollection<ToolKitStruct>(a)
                    });
                });
        }

        public ICommand GroupByBlockTypeCommand
        {
            get { return new RelayCommand(GroupByBlockTypeCommand_Execute); }
        }

        private void GroupByBlockTypeCommand_Execute()
        {
            ToolKitGroups.Clear();

            ToolKitStructs.GroupBy(a => a.BlockType)
                .OrderBy(a => a.Key)
                .ToList()
                .ForEach(a =>
                {
                    ToolKitGroups.Add(new ToolKitGroup
                    {
                        GroupName = a.Key.ToString(),
                        ToolKitStructs = new ObservableCollection<ToolKitStruct>(a)
                    });
                });
        }

        #endregion



        #region

        internal void UpdateProperty<T>(ref T properValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(properValue, newValue))
            {
                return;
            }

            properValue = newValue;

            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
