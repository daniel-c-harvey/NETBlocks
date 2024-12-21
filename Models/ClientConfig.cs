
namespace NetBlocks.Models
{
    public class ClientConfig
    {
        public string URL { get; }

        public ClientConfig(string baseURL, int port)
        {
            URL = $"{baseURL}:{port}";
        }

        public ClientConfig(string url)
        {
            URL = url;
        }

    }
}
