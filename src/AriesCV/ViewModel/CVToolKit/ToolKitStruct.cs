using System;
using System.Collections.ObjectModel;

namespace AriesCV.ViewModel.CVToolKit
{
    public class ToolKitStruct
    {

        public string Name { set; get; }

        public string Catetogy { set; get; }

        public string ICon { set; get; }

        public Type ClassType { set; get; }

        public ObservableCollection<ToolKitStruct> Children { set; get; }
    }
}