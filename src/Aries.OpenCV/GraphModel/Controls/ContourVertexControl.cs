using GraphX.Controls;

namespace Aries.OpenCV.GraphModel.Controls
{
    public class ContourVertexControl : VertexControl
    {
        public ContourVertexControl(object vertexData, bool tracePositionChange = true, bool bindToDataObject = true) :
            base(vertexData, tracePositionChange, bindToDataObject)
        {
        }
    }
}
