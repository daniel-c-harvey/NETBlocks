namespace NetBlocks.Utilities.Environment;

public class JsonTools<T>
{
    public static T LoadFromFile(string filePathFromBase)
    {
        using var reader = new StreamReader(File.OpenRead(filePathFromBase));
        
        string json = reader.ReadToEnd();
        var model = System.Text.Json.JsonSerializer.Deserialize<T>(json);
        reader.Close();
        return model ?? throw new Exception($"Could not read settings for {typeof(T).Name}");
    }

    public static void SaveToFile(string filePathFromBase, T model)
    {
        using var writer = new StreamWriter(File.Create(filePathFromBase));
        
        string json = System.Text.Json.JsonSerializer.Serialize(model);
        writer.WriteLine(json);
        writer.Close();
    }
}