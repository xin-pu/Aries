using System;
using System.Windows.Input;
using Aries.Utility;

namespace Aries.Core
{
    public class GraphStyleManager
    {

        private static readonly Lazy<GraphStyleManager> lazy =
            new Lazy<GraphStyleManager>(() => new GraphStyleManager());

        public static GraphStyleManager Instance
        {
            get { return lazy.Value; }
        }


        public bool IsShowEdgeLabels { set; get; }

        public bool IsAlignEdgeLabels { set; get; }

        public AriesMain AriesMain { set; get; }

        public ICommand ShowEdgeLabelCommand
        {
            get { return new RelayCommand(ShowEdgeLabelCommand_Execute); }
        }

        private void ShowEdgeLabelCommand_Execute()
        {
            AriesMain.GraphCvAreaAtWorkSpace.ShowAllEdgesLabels(IsShowEdgeLabels);
        }

        public ICommand AlignEdgeLabelsCommand
        {
            get { return new RelayCommand(AlignEdgeLabelsCommand_Execute); }
        }

        private void AlignEdgeLabelsCommand_Execute()
        {
            AriesMain.GraphCvAreaAtWorkSpace.AlignAllEdgesLabels(IsAlignEdgeLabels);
        }
    }
}
