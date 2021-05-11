﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Aries.OpenCV.Core;
using Aries.OpenCV.GraphModel;

namespace Aries.Core
{
    public class BlockToolKitManager
    {

        public List<ToolKitGroupByType> BlockToolKitStructs { set; get; }

        private static readonly Lazy<BlockToolKitManager> lazy =
            new Lazy<BlockToolKitManager>(() => new BlockToolKitManager());

        public static BlockToolKitManager Instance
        {
            get { return lazy.Value; }
        }

        public BlockToolKitManager()
        {
            BlockToolKitStructs = new List<ToolKitGroupByType>(0);
            Sync();
        }

        public void Sync()
        {
            BlockToolKitStructs.Clear();
            var types = BlockHelper.GetBlockClassType();
            var typeAll = types.Select(a => new ToolKitStruct
            {
                Name = a.Name,
                ClassType = a,
                BlockType = BlockHelper.GetBlockType(a)
            }).ToList();
            typeAll.GroupBy(a => a.BlockType).ToList().ForEach(a =>
            {
                BlockToolKitStructs.Add(new ToolKitGroupByType
                {
                    GroupName = a.Key.ToString(),
                    ToolKitStructs = new ObservableCollection<ToolKitStruct>(a)
                });
            });
        }

    }


    public class ToolKitGroupByType : INotifyPropertyChanged
    {

        private string _groupName;
        private ObservableCollection<ToolKitStruct> _toolKitStructs;

        public string GroupName
        {
            set { UpdateProperty(ref _groupName, value); }
            get { return _groupName; }
        }

        public ObservableCollection<ToolKitStruct> ToolKitStructs
        {
            set { UpdateProperty(ref _toolKitStructs, value); }
            get { return _toolKitStructs; }
        }


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

    public class ToolKitStruct : INotifyPropertyChanged
    {
        private string _name;
        private Type _classType;
        private BlockType _blockType;

        public string Name
        {
            set { UpdateProperty(ref _name, value); }
            get { return _name; }
        }

        public Type ClassType
        {
            set { UpdateProperty(ref _classType, value); }
            get { return _classType; }
        }

        public BlockType BlockType
        {
            set { UpdateProperty(ref _blockType, value); }
            get { return _blockType; }
        }

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
