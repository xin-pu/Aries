using System;
using System.Windows;
using System.Windows.Media;
using Aries.OpenCV.GraphModel.Core;
using GraphX.Controls;

namespace AriesCV.ViewModel
{
    public class GraphCVEditManager : IDisposable
    {
        private GraphCVArea _graphArea;
        private ZoomControl _zoomControl;
        private EdgeBlueprint _edgeBp;

        public GraphCVEditManager(GraphCVArea graphArea, ZoomControl zc)
        {
            _graphArea = graphArea;
            _zoomControl = zc;
            _zoomControl.MouseMove += _zoomControl_MouseMove;
        }

        public void CreateVirtualEdge(VertexControl source, Point mousePos)
        {
            _edgeBp = new EdgeBlueprint(source, Brushes.Black);
            _graphArea.InsertCustomChildControl(0, _edgeBp.EdgePath);
        }

        void _zoomControl_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_edgeBp == null) return;
            var pos = _zoomControl.TranslatePoint(e.GetPosition(_zoomControl), _graphArea);
            pos.Offset(2, 2);
            _edgeBp.UpdateTargetPosition(pos);
        }

        private void ClearEdgeBp()
        {
            if (_edgeBp == null) return;
            _graphArea.RemoveCustomChildControl(_edgeBp.EdgePath);
            _edgeBp.Dispose();
            _edgeBp = null;
        }

        public void Dispose()
        {
            ClearEdgeBp();
            _graphArea = null;
            if (_zoomControl != null)
                _zoomControl.MouseMove -= _zoomControl_MouseMove;
            _zoomControl = null;
        }

        public void DestroyVirtualEdge()
        {
            ClearEdgeBp();
        }
    }
}
