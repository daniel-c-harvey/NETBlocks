namespace NetBlocks.Models.Environment
{
    public interface IEndpoints
    {
        string MediaApiUrl { get; set; }
    }

    public class Endpoints : IEndpoints
    {
        public string MediaApiUrl { get; set; } = default!;
    }
}
