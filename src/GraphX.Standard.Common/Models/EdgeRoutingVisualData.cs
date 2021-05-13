using GraphX.Measure;
using GraphX.Common.Interfaces;

namespace GraphX.Common.Models
{
    public class EdgeRoutingVisualData
    {
        public bool HaveTemplate { get; set; }
        public bool IsEdgeVisible { get; set; }
        public bool IsEdgeSelfLooped { get; set; }

        public GPoint SourcePosition { get; set; }
        public Size SourceSize { get; set; }
        public GPoint TargetPosition { get; set; }
        public Size TargetSize { get; set; }

        IRoutingInfo Edge { get; set; }
    }
}
