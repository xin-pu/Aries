using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Aries.Utility;
using GraphX.Common.Enums;
using GraphX.Common.Interfaces;
using GraphX.Logic.Algorithms.LayoutAlgorithms;

namespace Aries.Core
{
    public class GraphStyleManager : INotifyPropertyChanged
    {

        private LogicCoreCV _logicCoreCv;
        public ILayoutParameters _layoutParameters;
        public LayoutAlgorithmTypeEnum _layoutAlgorithm;

        public GraphCVArea GraphCvArea { set; get; }

        public LogicCoreCV LogicCoreCv
        {
            set { UpdateProperty(ref _logicCoreCv, value); }
            get { return _logicCoreCv; }
        }

        public LayoutAlgorithmTypeEnum LayoutAlgorithm
        {
            set { UpdateProperty(ref _layoutAlgorithm, value); }
            get { return _layoutAlgorithm; }
        }


        public ILayoutParameters LayoutParameters
        {
            set { UpdateProperty(ref _layoutParameters, value); }
            get { return _layoutParameters; }
        }

        public GraphStyleManager(GraphCVArea graphCvArea)
        {
            GraphCvArea = graphCvArea;
            LogicCoreCv = GraphCvArea.GetLogicCore<LogicCoreCV>();
            LayoutAlgorithm = LogicCoreCv.DefaultLayoutAlgorithm;
            LayoutParameters = LogicCoreCv.DefaultLayoutAlgorithmParams;
        }


        public bool IsShowEdgeLabels { set; get; }

        public bool IsAlignEdgeLabels { set; get; }



        #region Command

        public ICommand ShowEdgeLabelCommand
        {
            get { return new RelayCommand(ShowEdgeLabelCommand_Execute); }
        }


        public ICommand AlignEdgeLabelsCommand
        {
            get { return new RelayCommand(AlignEdgeLabelsCommand_Execute); }
        }

        public ICommand RelayoutGraphCommand
        {
            get { return new RelayCommand(RelayoutGraphCommand_Execute); }
        }


        public ICommand LayoutAlgorithmTypeEnumChangeCommand
        {
            get { return new RelayCommand(LayoutAlgorithmTypeEnumChangeCommand_Execute); }
        }


        private void ShowEdgeLabelCommand_Execute()
        {
            GraphCvArea.ShowAllEdgesLabels(IsShowEdgeLabels);
        }


        private void AlignEdgeLabelsCommand_Execute()
        {
            GraphCvArea.AlignAllEdgesLabels(IsAlignEdgeLabels);
        }

        private void RelayoutGraphCommand_Execute()
        {
            GraphCvArea.RelayoutGraph();
        }

        private void LayoutAlgorithmTypeEnumChangeCommand_Execute()
        {
            switch (LayoutAlgorithm)
            {
                case LayoutAlgorithmTypeEnum.BoundedFR:
                    break;
                case LayoutAlgorithmTypeEnum.Circular:
                    LayoutParameters = CircularLayoutParameters;
                    break;
                case LayoutAlgorithmTypeEnum.CompoundFDP:
                    break;
                case LayoutAlgorithmTypeEnum.EfficientSugiyama:
                    break;
                case LayoutAlgorithmTypeEnum.Sugiyama:
                    break;
                case LayoutAlgorithmTypeEnum.FR:
                    break;
                case LayoutAlgorithmTypeEnum.ISOM:
                    break;
                case LayoutAlgorithmTypeEnum.KK:
                    break;
                case LayoutAlgorithmTypeEnum.LinLog:
                    break;
                case LayoutAlgorithmTypeEnum.Tree:
                    LayoutParameters = SimpleTreeLayoutParameters;
                    break;
                case LayoutAlgorithmTypeEnum.SimpleRandom:
                    break;
                case LayoutAlgorithmTypeEnum.Custom:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            GraphCvArea.LogicCore.DefaultLayoutAlgorithm = LayoutAlgorithm;
            GraphCvArea.LogicCore.DefaultLayoutAlgorithmParams = LayoutParameters;
        }


        public SimpleTreeLayoutParameters SimpleTreeLayoutParameters { set; get; } = new SimpleTreeLayoutParameters();

        public CircularLayoutParameters CircularLayoutParameters { set; get; } = new CircularLayoutParameters();

        #endregion



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
