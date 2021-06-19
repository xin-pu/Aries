﻿using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace AriesCV.ViewModel
{
    public class MenuLayoutModel : ViewModelBase
    {

        //private bool _isShowEdgeLabels;
        //private bool _isAlignEdgeLabels;
        //public LayoutCategory _layoutCategory;
        //public EdgeRoutingCategory _edgeRoutingCategory;

        private GraphCVLayoutConfig _graphCvLayoutConfig;

        public GraphCVLayoutConfig GraphCvLayoutConfig
        {
            get { return _graphCvLayoutConfig; }
            set
            {
                _graphCvLayoutConfig = value;
                RaisePropertyChanged(() => GraphCvLayoutConfig);
            }
        }

        //public LayoutCategory LayoutCategory
        //{
        //    get { return _layoutCategory; }
        //    set
        //    {
        //        _layoutCategory = value;
        //        RaisePropertyChanged(() => IsShowEdgeLabels);
        //    }
        //}
        //public EdgeRoutingCategory EdgeRoutingCategory
        //{
        //    get { return _edgeRoutingCategory; }
        //    set
        //    {
        //        _edgeRoutingCategory = value;
        //        RaisePropertyChanged(() => IsShowEdgeLabels);
        //    }
        //}
        //public bool IsShowEdgeLabels
        //{
        //    get { return _isShowEdgeLabels; }
        //    set
        //    {
        //        _isShowEdgeLabels = value;
        //        RaisePropertyChanged(() => IsShowEdgeLabels);
        //    }
        //}

        //public bool IsAlignEdgeLabels
        //{
        //    get { return _isAlignEdgeLabels; }
        //    set
        //    {
        //        _isAlignEdgeLabels = value;
        //        RaisePropertyChanged(() => IsAlignEdgeLabels);
        //    }
        //}

        #region Command

        public RelayCommand ShowEdgeLabelCommand =>
            new Lazy<RelayCommand>(() =>
                new RelayCommand(ShowEdgeLabelCommand_Execute, CanSetLayout)).Value;


        public RelayCommand AlignEdgeLabelsCommand =>
            new Lazy<RelayCommand>(() =>
                new RelayCommand(AlignEdgeLabelsCommand_Execute, CanSetLayout)).Value;



        public RelayCommand LayoutCategorySelectedChangeCommand =>
            new Lazy<RelayCommand>(() =>
                new RelayCommand(LayoutCategorySelectedChangeCommand_Execute, CanSetLayout)).Value;


        public RelayCommand EdgeRoutingCategorySelectedChangeCommand =>
            new Lazy<RelayCommand>(() =>
                new RelayCommand(EdgeRoutingCategorySelectedChangeCommand_Execute, CanSetLayout)).Value;



        public RelayCommand RelayoutGraphCommand =>
            new Lazy<RelayCommand>(() =>
                new RelayCommand(RelayoutGraphCommand_Execute)).Value;


        private bool CanSetLayout()
        {
            return GraphCvLayoutConfig != null;
        }

        private void LayoutCategorySelectedChangeCommand_Execute()
        {
            Messenger.Default.Send(GraphCvLayoutConfig.LayoutAlgorithm, "ReSetLayoutCategoryToken");
        }


        private void EdgeRoutingCategorySelectedChangeCommand_Execute()
        {
            Messenger.Default.Send(GraphCvLayoutConfig.EdgeRoutingType, "ResetEdgeRoutingCategoryToken");
        }

        private void ShowEdgeLabelCommand_Execute()
        {
            Messenger.Default.Send(GraphCvLayoutConfig.IsShowEdgeLabels, "ResetShowEdgeLabelToken");
        }


        private void AlignEdgeLabelsCommand_Execute()
        {
            Messenger.Default.Send(GraphCvLayoutConfig.IsAlignEdgeLabels, "ResetAlignEdgeLabelToken");
        }

        private void RelayoutGraphCommand_Execute()
        {
            Messenger.Default.Send(GraphCvLayoutConfig.IsAlignEdgeLabels, "RelayoutGraphToken");
        }

        #endregion

    }
}
