using System;
using System.Windows.Input;
using Aries.Utility;

namespace Aries.OpenCV.GraphModel
{
    public class MatRecord : IDisposable
    {
        public long ParentId { set; get; }
        public string ParentName { set; get; }
        public string PropertyName { set; get; }
        public string FileName { set; get; }

        public DateTime UpDateTime { set; get; }

        public ICommand OpenImageCommand =>
            new RelayCommand(OpenImageCommand_Execute);

        private void OpenImageCommand_Execute()
        {

        }

        public void Dispose()
        {

        }
    }
}