using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Aries.OpenCV.Core;
using Aries.Utility;

namespace Aries.Core
{
    public class ToolKitStruct : INotifyPropertyChanged
    {
        private string _name;
        public string _icon;
        private Type _classType;
        private string _blockType;
        private string _catetogyType;
        public GraphCVArea _graphCVAreaAtWorkSpace;

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

        public string CatetogyType
        {
            set { UpdateProperty(ref _catetogyType, value); }
            get { return _catetogyType; }
        }

        public string BlockType
        {
            set { UpdateProperty(ref _blockType, value); }
            get { return _blockType; }
        }

        public GraphCVArea GraphCVAreaAtWorkSpace
        {
            set { UpdateProperty(ref _graphCVAreaAtWorkSpace, value); }
            get { return _graphCVAreaAtWorkSpace; }
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
            GraphCVAreaAtWorkSpace?.AddBlock(block);
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