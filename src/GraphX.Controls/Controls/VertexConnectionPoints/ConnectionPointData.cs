namespace GraphX.Controls
{
    public class ConnectionPointData
    {
        public long ParentID { set; get; }
        public int ID { set; get; }
        public string Header { set; get; }
        public ConnectionPointType ConnectionPointType { set; get; }
    }


    public enum ConnectionPointType
    {
        IN,
        OUT
    }
}