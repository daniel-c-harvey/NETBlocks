using System.Text.Json;
using System.IO;

namespace NetBlocks.Utilities.Environment;

public static class ConfigLoader
{
    public static T LoadFromFile<T>(string fileName)
    {
        return JsonSerializer.Deserialize<T>(File.ReadAllText(fileName))
            ?? throw new InvalidOperationException($"Failed to deserialize {fileName} to type {typeof(T).Name}");
    }
}