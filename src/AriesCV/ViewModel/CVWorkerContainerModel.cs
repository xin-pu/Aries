using System.Collections.Generic;
using System.Linq;
using Aries.OpenCV.GraphModel;
using AriesCV.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GraphX.Controls;

namespace AriesCV.ViewModel
{


    public class CVWorkerContainerModel : ViewModelBase
    {

        private ZoomControl _zoomControl;
        public CVWorkerItemView _cvWorkerItemView;

        public CVWorkerContainerModel()
        {
            Messenger.Default.Register<BlockVertex>(this, "AddBlockToken", AddBlockVertex);
            Messenger.Default.Register<CVWorkerItemView>(this, "AddCVWorkerModelToken", AddCVWorkerModel);
            Messenger.Default.Register<string>(this, "RemoveCVWorkerModelToken", RemoveCVWorkerModel);
            Messenger.Default.Register<string>(this, "RemoveAllCVWorkerModelToken", RemoveAllCVWorkerModel);
        }

        public GraphCVArea GraphCvAreaAtWorkSpace { set; get; }

        public ZoomControl ZoomControl
        {
            get { return _zoomControl; }
            set
            {
                _zoomControl = value;
                RaisePropertyChanged(() => ZoomControl);
            }
        }

        public CVWorkerItemView CvWorkerItemView
        {
            get { return _cvWorkerItemView; }
            set
            {
                _cvWorkerItemView = value;
                RaisePropertyChanged(() => CvWorkerItemView);
            }
        }

        public Dictionary<string, CVWorkerItemView> CvWorkerItemViewDict =
            new Dictionary<string, CVWorkerItemView>();

        public List<string> CurrentKeys => CvWorkerItemViewDict.Keys.ToList();

        public RelayCommand<object> SelectWorkUnitCommand
        {
            get { return new RelayCommand<object>(SelectWorkUnitCommand_Execute); }
        }

        private void AddBlockVertex(BlockVertex obj)
        {
            GraphCvAreaAtWorkSpace?.AddBlock(obj);
        }

        private void SelectWorkUnitCommand_Execute(object obj)
        {
            if (obj == null)
                return;
            CvWorkerItemView = (CVWorkerItemView) obj;
            GraphCvAreaAtWorkSpace = CvWorkerItemView.GraphCVArea;
            ZoomControl = CvWorkerItemView.ZoomControl;
        }



        public void AddCVWorkerModel(CVWorkerItemView cvWorkerItemView)
        {
            CvWorkerItemViewDict[cvWorkerItemView.Name] = cvWorkerItemView;
        }

        public void RemoveCVWorkerModel(string name)
        {
            CvWorkerItemViewDict[name].Dispose();
            CvWorkerItemViewDict.Remove(name);
        }

        public void RemoveAllCVWorkerModel(string message)
        {
            foreach (var cvWorkerItemView in CvWorkerItemViewDict.Values)
            {
                cvWorkerItemView.Dispose();
            }
            CvWorkerItemViewDict.Clear();
        }


    }
}
