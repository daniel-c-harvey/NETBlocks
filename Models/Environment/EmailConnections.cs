namespace NetBlocks.Models.Environment;

// public interface IEmailConnections
// {
//     int ActiveConnectionID { get; set; }
//     List<EmailConnection> Services { get; set; }
// }

public class EmailConnections// : IEmailConnections
{
    public int ActiveConnectionID { get; set; }
    public List<EmailConnection> Connections { get; set; }
}