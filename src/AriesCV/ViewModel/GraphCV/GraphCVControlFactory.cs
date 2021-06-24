using Aries.OpenCV.GraphModel;
using Aries.OpenCV.GraphModel.Controls;
using GraphX.Controls;
using GraphX.Controls.Models;

namespace AriesCV.ViewModel
{
    public class GraphCVControlFactory : GraphControlFactory
    {
        public GraphCVControlFactory(GraphAreaBase graphArea) : base(graphArea)
        {
        }

        public override VertexControl CreateVertexControl(object vertexData)
        {
            var type = vertexData.GetType();
            if (type.IsSubclassOf(typeof(VertexMat)))
            {
                return new MatVertexControl(vertexData)
                {
                    RootArea = FactoryRootArea
                };
            }

            if (type.IsSubclassOf(typeof(VertexContour)))
            {
                return new ContourVertexControl(vertexData)
                {
                    RootArea = FactoryRootArea
                };
            }

            return base.CreateVertexControl(vertexData);
        }
    }
}
