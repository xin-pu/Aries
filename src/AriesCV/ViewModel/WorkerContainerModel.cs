using System;
using System.Collections.Generic;
using System.Linq;
using Aries.OpenCV;
using Aries.OpenCV.GraphModel;
using Aries.OpenCV.GraphModel.Core;
using AriesCV.ViewModel.GraphLayout;
using AriesCV.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GraphX.Controls;

namespace AriesCV.ViewModel
{


    public class WorkerContainerModel : ViewModelBase
    {

        private ZoomControl _zoomControl;
        public CVWorkerItemView _cvWorkerItemView;
        public GraphCVArea _graphCvAreaAtWorkSpace;

        public WorkerContainerModel()
        {
            RegisterForToolKit();
            RegisterForFile();
            RegisterForLayout();
            RegisterForRunner();

        }


        private void RegisterForToolKit()
        {
            Messenger.Default.Register<Type>(this, "AddBlockToken", AddMatBlockVertex);
        }

        private void RegisterForFile()
        {
            Messenger.Default.Register<CVWorkerItemView>(this, "AddCVWorkerModelToken", AddCVWorkerModel);
            Messenger.Default.Register<string>(this, "RemoveCVWorkerModelToken", RemoveCVWorkerModel);
            Messenger.Default.Register<string>(this, "RemoveAllCVWorkerModelToken", RemoveAllCVWorkerModel);
        }



        /// <summary>
        /// Works for Item Select
        /// </summary>
        private void RegisterForLayout()
        {
            Messenger.Default.Register<LayoutType>(this, "ReSetLayoutCategoryToken", ReSetLayoutCategory);
            Messenger.Default.Register<EdgeRoutingType>(this, "ResetEdgeRoutingCategoryToken",
                ResetEdgeRoutingCategory);
            Messenger.Default.Register<bool>(this, "ResetShowEdgeLabelToken", ResetShowEdgeLabel);
            Messenger.Default.Register<bool>(this, "ResetAlignEdgeLabelToken", ResetAlignEdgeLabel);
            Messenger.Default.Register<bool>(this, "ReSetShowImageViewToken", ResetShowImageView);
            Messenger.Default.Register<string>(this, "RelayoutGraphToken", RelayoutGraph);
        }

        /// <summary>
        /// Works for Item Select
        /// </summary>
        private void RegisterForRunner()
        {
            Messenger.Default.Register<string>(this, "RunGraphByDataToken", RunGraphByData);
            Messenger.Default.Register<string>(this, "ReloadGraphToken", ReloadGraph);
            Messenger.Default.Register<bool>(this, "SetEnableSaveImageToken", SetEnableSaveImage);

            Messenger.Default.Register<string>(this, "OpenWorkDirectoryToken", OpenWorkDirectory);
            Messenger.Default.Register<string>(this, "ChangeWorkDirectoryToken", ChangeWorkDirectory);
        }






        public GraphCVArea GraphCvAreaAtWorkSpace
        {
            get { return _graphCvAreaAtWorkSpace; }
            set
            {
                _graphCvAreaAtWorkSpace = value;
                RaisePropertyChanged(() => GraphCvAreaAtWorkSpace);
            }
        }

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

        private void AddMatBlockVertex(Type type)
        {
            if (CvWorkerItemView == null)
                return;

            if (type.IsSubclassOf(typeof(VertexMat)))
            {
                var vertex = BlockHelper.CreateVertex<VertexMat>(type);
                var workDirectory = CvWorkerItemView.GraphCvRunConfig.WorkDirectory;
                vertex.WorkDirectory = workDirectory;
                GraphCvAreaAtWorkSpace?.AddMatBlock(vertex);

            }
            else if (type.IsSubclassOf(typeof(VertexContour)))
            {
                var vertex = BlockHelper.CreateVertex<VertexContour>(type);
                var workDirectory = CvWorkerItemView.GraphCvRunConfig.WorkDirectory;
                vertex.WorkDirectory = workDirectory;
                GraphCvAreaAtWorkSpace?.AddContourBlock(vertex);
            }
          
        }

        private void SelectWorkUnitCommand_Execute(object obj)
        {
            if (obj == null)
                return;
            CvWorkerItemView = (CVWorkerItemView) obj;
            GraphCvAreaAtWorkSpace = CvWorkerItemView.GraphCVArea;
            ZoomControl = CvWorkerItemView.ZoomControl;
            ViewModelLocator.Instance.MenuLayout.GraphCvLayoutConfig = CvWorkerItemView.GraphCvLayoutConfig;
            ViewModelLocator.Instance.MenuRunner.GraphCVRunConfig = CvWorkerItemView.GraphCvRunConfig;
        }


        #region File

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

        #endregion




        #region Runner

        public async void RunGraphByData(string obj)
        {
            await CvWorkerItemView.RunGraphByDataAsync();
        }

        public async void ReloadGraph(string obj)
        {
            await CvWorkerItemView.ReloadAllBlockAsync();
        }

        public async void SetEnableSaveImage(bool isEnable)
        {
            await CvWorkerItemView.SetEnableSaveImageAsync(isEnable);
        }

        public  void OpenWorkDirectory(string obj)
        {
            CvWorkerItemView.OpenWorkDirectory();
        }

        public  void ChangeWorkDirectory(string obj)
        {
            CvWorkerItemView.ChangeWorkDirectory();
        }

        #endregion


        #region Layout

        private void ReSetLayoutCategory(LayoutType layoutType)
        {
            CvWorkerItemView.ReSetLayoutCategory(layoutType);
        }

        private void ResetEdgeRoutingCategory(EdgeRoutingType edgeRoutingType)
        {
            CvWorkerItemView.ResetEdgeRoutingCategory(edgeRoutingType);
        }

        private void ResetShowEdgeLabel(bool obj)
        {
            CvWorkerItemView.ResetShowEdgeLabel(obj);
        }

        private void ResetAlignEdgeLabel(bool obj)
        {
            CvWorkerItemView.ResetAlignEdgeLabel(obj);
        }

        private void ResetShowImageView(bool obj)
        {
            CvWorkerItemView.ResetShowImageView(obj);
        }

        private void RelayoutGraph(string obj)
        {
            CvWorkerItemView.RelayoutGraph();
        }

        #endregion


    }
}
