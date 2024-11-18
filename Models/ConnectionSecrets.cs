namespace SnailbirdWeb.Models
{
    public class ConnectionSecrets
    {
        public string ActiveConnectionName { get; set; } = default!;
        public IEnumerable<Connection> Connections { get; set; } = default!;
    }

    public class Connection
    {
        public string ConnectionName { get; set; } = default!;
        public string DatabaseName {  get; set; } = default!;
        public string ConnectionString { get; set; } = default!;
    }
}
