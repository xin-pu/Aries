using GraphX.Controls;

namespace Aries.OpenCV.GraphModel.Controls
{
    public class MatsVertexControl : VertexControl
    {
        public MatsVertexControl(object vertexData, bool tracePositionChange = true, bool bindToDataObject = true) :
            base(vertexData, tracePositionChange, bindToDataObject)
        {
        }
    }
}
