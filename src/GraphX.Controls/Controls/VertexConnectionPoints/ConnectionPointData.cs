using System;

namespace GraphX.Controls
{
    public class ConnectionPointData
    {
        public long ParentID { set; get; }
        public int ID { set; get; }
        public string Header { set; get; }
        public string Icon { set; get; }
        public string TypeFullName { set; get; }
        public ConnectType ConnectType { set; get; }

    }

    public enum ConnectType
    {
        INPUT,
        OUTPUT
    }
}