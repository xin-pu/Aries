using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Aries.Utility;

namespace Aries.Core
{
    public class ImageRecordManager
    {



        public ObservableCollection<ImageRecord> ImageRecords { set; get; }


        public void AppendImageRecord(ImageRecord imageRecord)
        {
            ImageRecords.Add(imageRecord);
        }

        public void ClearImageRecord()
        {
            ImageRecords.Clear();
        }

    }


    public class ImageRecord
    {
        public int ParentId { set; get; }
        public string ParentName { set; get; }
        public string FileName { set; get; }

        public DateTime UpDateTime { set; get; }

        public ICommand OpenImageCommand => new RelayCommand(OpenImageCommand_Execute);

        private void OpenImageCommand_Execute()
        {
            throw new NotImplementedException();
        }
    }

}
