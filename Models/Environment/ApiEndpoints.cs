namespace NetBlocks.Models.Environment;

public class ApiEndpoints
{
    public IEnumerable<ApiEndpoint> Endpoints { get; set; } = [];
}

public class ApiEndpoint
{
    public string Name { get; set; }
    public string ApiUrl { get; set; }
    public string? ApiKey { get; set; }
}