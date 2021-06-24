using GraphX.Controls;

namespace Aries.OpenCV.GraphModel.Controls
{
    public class MatVertexControl : VertexControl
    {
        public MatVertexControl(object vertexData, bool tracePositionChange = true, bool bindToDataObject = true) :
            base(vertexData, tracePositionChange, bindToDataObject)
        {
        }
    }
}
