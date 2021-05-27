namespace GraphX.Controls
{
    public class ConnectionPointData
    {
        public long ParentID { set; get; }
        public int ID { set; get; }
        public string Header { set; get; }
        public ConnectType ConnectType { set; get; }
    }

    public enum ConnectType
    {
        IN_MAT,
        OUT_MAT
    }
}