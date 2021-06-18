using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

namespace AriesCV.ViewModel
{
    public class MenuLayoutModel : ViewModelBase
    {

        private GraphCVLayoutConfig _graphCvLayoutConfig;

        public GraphCVLayoutConfig GraphCvLayoutConfig {
            get { return _graphCvLayoutConfig; }
            set
            {
                _graphCvLayoutConfig = value;
                RaisePropertyChanged(() => GraphCvLayoutConfig);
            }
        }

        

        #region Command

        public RelayCommand ShowEdgeLabelCommand
        {
            get { return new RelayCommand(ShowEdgeLabelCommand_Execute); }
        }

        public RelayCommand AlignEdgeLabelsCommand
        {
            get { return new RelayCommand(AlignEdgeLabelsCommand_Execute); }
        }


        public RelayCommand LayoutCategorySelectedChangeCommand
        {
            get { return new RelayCommand(LayoutCategorySelectedChangeCommand_Execute); }
        }

        public RelayCommand EdgeRoutingCategorySelectedChangeCommand
        {
            get { return new RelayCommand(EdgeRoutingCategorySelectedChangeCommand_Execute); }
        }


        public RelayCommand RelayoutGraphCommand
        {
            get { return new RelayCommand(RelayoutGraphCommand_Execute); }
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
            Messenger.Default.Send(GraphCvLayoutConfig.IsAlignEdgeLabels, "ResetAlignEdgeLabel");
        }

        private void RelayoutGraphCommand_Execute()
        {
         
        }



        #endregion

    }
}
