namespace NetBlocks.Models.Environment;

public class EmailConnection
{
    public int ID { get; set; }
    public string ServiceName { get; set; }
    public string FromAddress { get; set; }
    public string Host { get; set; }
    public string Token { get; set; }
    public string? TestInbox { get; set; }
}