using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Aries.Utility;
using GraphX.Common.Enums;
using GraphX.Common.Interfaces;
using GraphX.Logic.Algorithms.LayoutAlgorithms;
using GraphX.Measure;

namespace Aries.Core
{
    public class GraphStyleManager : INotifyPropertyChanged
    {

        private LogicCoreCV _logicCoreCv;
        public ILayoutParameters _layoutParameters;

        public GraphCVArea GraphCvArea { set; get; }

        public LogicCoreCV LogicCoreCv
        {
            set { UpdateProperty(ref _logicCoreCv, value); }
            get { return _logicCoreCv; }
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
        }



        public bool IsShowEdgeLabels { set; get; }

        public bool IsAlignEdgeLabels { set; get; }


        public ICommand ShowEdgeLabelCommand
        {
            get { return new RelayCommand(ShowEdgeLabelCommand_Execute); }
        }

        private void ShowEdgeLabelCommand_Execute()
        {
            GraphCvArea.ShowAllEdgesLabels(IsShowEdgeLabels);
        }

        public ICommand AlignEdgeLabelsCommand
        {
            get { return new RelayCommand(AlignEdgeLabelsCommand_Execute); }
        }

        private void AlignEdgeLabelsCommand_Execute()
        {
            GraphCvArea.AlignAllEdgesLabels(IsAlignEdgeLabels);
        }


        public ICommand RelayoutGraphCommand
        {
            get { return new RelayCommand(RelayoutGraphCommand_Execute); }
        }

        private void RelayoutGraphCommand_Execute()
        {

            GraphCvArea.RelayoutGraph();
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
