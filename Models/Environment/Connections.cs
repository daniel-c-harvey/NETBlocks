namespace NetBlocks.Models.Environment
{
    public class Connections
    {
        public int ActiveConnectionID { get; set; } = 0;
        public IList<Connection> ConnectionStrings { get; set; } = [];
    }
}