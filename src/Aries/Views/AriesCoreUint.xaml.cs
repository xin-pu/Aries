using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Aries.Core;
using Aries.OpenCV.GraphModel;
using GraphX;
using GraphX.Common.Enums;
using GraphX.Controls;
using GraphX.Controls.Animations;
using GraphX.Controls.Models;

namespace Aries.Views
{
    /// <summary>
    /// Interaction logic for AriesCoreUint.xaml
    /// </summary>
    public partial class AriesCoreUint
    {

        /// <summary>
        /// Create For Open Command
        /// </summary>
        /// <param name="graphCvCore"></param>
        public AriesCoreUint(GraphCVCore graphCvCore)
        {
            InitializeComponent();
            GraphCvCore = graphCvCore;
            Initial();
            DataContext = this;
        }

        public GraphCVCore GraphCvCore { set; get; }

        
       

        private void Initial()
        {
            GraphCvCore.ZoomControl = ZoomControl;
            GraphCvCore.GraphCvArea = GraphArea;
            InitialGraphArea();
            InitialGraphLogic();
        }

        private void InitialGraphArea()
        {
            GraphArea.LogicCore = new LogicCoreCV
            {
                DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.KK,
                DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA,
                DefaultOverlapRemovalAlgorithmParams = { VerticalGap = 50 },
                DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None,
                EdgeCurvingEnabled = true,

            };
            GraphArea.MoveAnimation = AnimationFactory.CreateMoveAnimation(MoveAnimation.Move, TimeSpan.FromSeconds(0.5));
            GraphArea.MoveAnimation.Completed += MoveAnimationCompleted;
            GraphArea.VertexSelected += BlockVertexSelected;
        }

        private void InitialGraphLogic()
        {

        }

        private void MoveAnimationCompleted(object sender, EventArgs e)
        {
            ZoomControl.ZoomToFill();
        }

        private void BlockVertexSelected(object sender, VertexSelectedEventArgs args)
        {
            GraphCvCore.SelectBlockVertex = args.VertexControl.GetDataVertex<BlockVertex>();

            //args.VertexControl.ContextMenu = new ContextMenu();
            //var mi = new MenuItem { Header ="123" };
            //args.VertexControl.ContextMenu.Items.Add(mi);
            //args.VertexControl.ContextMenu.IsOpen = true;
        }

        /// <summary>
        /// Select vertex by setting its tag and highlight value
        /// </summary>
        /// <param name="vc">VertexControl object</param>
        private void SelectVertex(DependencyObject vc)
        {
            if (DragBehaviour.GetIsTagged(vc))
            {
                HighlightBehaviour.SetHighlighted(vc, false);
                DragBehaviour.SetIsTagged(vc, false);
                vc.ClearValue(DragBehaviour.XSnapModifierProperty);
                vc.ClearValue(DragBehaviour.YSnapModifierProperty);
            }
            else
            {
                HighlightBehaviour.SetHighlighted(vc, true);
                DragBehaviour.SetIsTagged(vc, true);
                DragBehaviour.SetXSnapModifier(vc, ExaggeratedSnappingXModifier);
                DragBehaviour.SetYSnapModifier(vc, ExaggeratedSnappingYModifier);
            }
        }

        private double ExaggeratedSnappingXModifier(GraphAreaBase area, DependencyObject obj, double val)
        {
            //if (dg_snapExaggerate.IsChecked ?? false)
            //{
            //    return System.Math.Round(val * 0.01) * 100.0;
            //}
            return DragBehaviour.GlobalXSnapModifier(area, obj, val);
        }

        private double ExaggeratedSnappingYModifier(GraphAreaBase area, DependencyObject obj, double val)
        {
            //if (dg_snapExaggerate.IsChecked ?? false)
            //{
            //    return System.Math.Round(val * 0.01) * 100.0;
            //}
            return DragBehaviour.GlobalYSnapModifier(area, obj, val);
        }
    }
}
