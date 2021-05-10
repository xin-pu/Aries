using System;
using System.ComponentModel;
using GraphX.Common.Models;
using GraphX.Controls;
using GraphX.Measure;
using QuickGraph;

namespace Aries
{
    public class GraphAreaExample : GraphArea<DataVertex, DataEdge, BidirectionalGraph<DataVertex, DataEdge>>
    {

    }
 

    public class DataVertex : VertexBase
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int ImageId { get; set; }

        public bool IsBlue { get; set; }

        #region Calculated or static props

        public override string ToString()
        {
            return Text;
        }

        #endregion

        /// <summary>
        /// Default constructor for this class
        /// (required for serialization).
        /// </summary>
        public DataVertex() : this(string.Empty)
        {
        }

        public DataVertex(string text = "")
        {
            Text = string.IsNullOrEmpty(text) ? "New Vertex" : text;
        }
    }

    [Serializable]
    public class DataEdge : EdgeBase<DataVertex>, INotifyPropertyChanged
    {
        //[YAXCustomSerializer(typeof(YAXPointArraySerializer))]
        public override Point[] RoutingPoints { get; set; }

        public DataEdge(DataVertex source, DataVertex target, double weight = 1)
            : base(source, target, weight)
        {
            Angle = 90;
        }

        public DataEdge()
            : base(null, null, 1)
        {
            Angle = 90;
        }

        public bool ArrowTarget { get; set; }

        public double Angle { get; set; }

        /// <summary>
        /// Node main description (header)
        /// </summary>
        private string _text;
        public string Text { get { return _text; } set { _text = value; OnPropertyChanged("Text"); } }
        public string ToolTipText { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
