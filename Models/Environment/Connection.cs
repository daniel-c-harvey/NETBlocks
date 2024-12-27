namespace NetBlocks.Models.Environment
{
    public class Connection
    {
        public long ID { get; set; } = 0;
        public string ConnectionName { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty; // todo this shouldn't be in the same cardinality as the connection string, which is per server
        public string ConnectionString { get; set; } = string.Empty;
    }
}