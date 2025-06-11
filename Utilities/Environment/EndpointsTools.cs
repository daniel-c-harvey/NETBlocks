using NetBlocks.Models.Environment;
using static System.Text.Json.JsonSerializer;

namespace NetBlocks.Utilities.Environment;

public static class EndpointsTools
{
    public static ApiEndpoints? LoadFromFile(string filePathFromBase)
    {
        using var reader = new StreamReader(File.OpenRead(filePathFromBase));
        if (reader is not null)
        {
            string json = reader.ReadToEnd();
            ApiEndpoints? endpoints = Deserialize<ApiEndpoints>(json);
            reader.Close();
            return endpoints;
        }
        throw new Exception($"Failed to open file reader: {filePathFromBase}");
    }

    public static ApiEndpoint LoadFromFile(string filePathFromBase, string endpointName)
    {
        ApiEndpoints? endpoints = LoadFromFile(filePathFromBase);
        ApiEndpoint? endpoint = endpoints?.Endpoints.FirstOrDefault(e => e.Name == endpointName);
        return endpoint ?? throw new Exception($"Failed to load endpoint {endpointName}");
    }

    public static void SaveToFile(string filePathFromBase, ApiEndpoints endpoints)
    {
        using var writer = new StreamWriter(File.Create(filePathFromBase));
        if (writer is not null)
        {
            string json = Serialize(endpoints);
            writer.WriteLine(json);
            writer.Close();
        }
    }
}