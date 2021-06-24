using System;
using GraphX.Common.Models;

namespace Aries.OpenCV.GraphModel
{
    [Serializable]
    public class BlockEdge : EdgeBase<VertexBasic>
    {
        public BlockEdge(VertexBasic source, VertexBasic target, double weight = 1)
            : base(source, target, weight)
        {

        }

        public BlockEdge()
            : base(null, null)
        {

        }

        private string _header;

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                RaisePropertyChanged(() => Header);
            }
        }



        public override string ToString()
        {
            return $"{Header}";
        }

    }
}
