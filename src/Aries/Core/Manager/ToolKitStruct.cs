﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Aries.OpenCV.Core;
using Aries.OpenCV.GraphModel;
using Aries.Utility;

namespace Aries.Core
{
    public class ToolKitStruct : INotifyPropertyChanged
    {
        private string _name;
        public string _icon;
        private Type _classType;
        private BlockType _blockType;
        public GraphCVCore _graphCvCoreAtWorkSpace;

        public string Name
        {
            set { UpdateProperty(ref _name, value); }
            get { return _name; }
        }

        public string Icon
        {
            set { UpdateProperty(ref _icon, value); }
            get { return _icon; }
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

        public GraphCVCore GraphCvCoreAtWorkSpace
        {
            set { UpdateProperty(ref _graphCvCoreAtWorkSpace, value); }
            get { return _graphCvCoreAtWorkSpace; }
        }

        public ICommand CreateBlockCommand
        {
            get { return new RelayCommand(CreateBlockCommand_Execute, CreateBlockCommand_CanExecute); }
        }

        private bool CreateBlockCommand_CanExecute()
        {
            return true;
        }

        private void CreateBlockCommand_Execute()
        {
            var block = BlockHelper.CreateBlockVertex(ClassType);
            GraphCvCoreAtWorkSpace?.AddBlock(block);
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