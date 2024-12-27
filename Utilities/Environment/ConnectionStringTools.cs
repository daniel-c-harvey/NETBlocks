using NetBlocks.Models.Environment;
using Newtonsoft.Json;

namespace NetBlocks.Utilities.Environment
{
    public class ConnectionStringLoader : IConnectionStringLoader
    {
        public Connections? LoadFromFile(string filePathFromBase)
        {
            return ConnectionStringTools.LoadFromFile(filePathFromBase);
        }

        public Connection LoadFromFile(string filePathFromBase, string connectionName)
        {
            return ConnectionStringTools.LoadFromFile(filePathFromBase, connectionName);
        }

        public void SaveToFile(string filePathFromBase, Connections connections)
        {
            ConnectionStringTools.SaveToFile(filePathFromBase, connections);
        }
    }

    public static class ConnectionStringTools
    {
        public static Connections? LoadFromFile(string filePathFromBase)
        {
            using var reader = new StreamReader(File.OpenRead(filePathFromBase));
            if (reader is not null)
            {
                string json = reader.ReadToEnd();
                Connections? connections = JsonConvert.DeserializeObject<Connections>(json);
                reader.Close();
                return connections;
            }
            throw new Exception($"Failed to open file reader: {filePathFromBase}");
        }

        public static Connection LoadFromFile(string filePathFromBase, string connectionName)
        {
            Connections? connections = LoadFromFile(filePathFromBase);
            Connection? connection = connections?.ConnectionStrings.FirstOrDefault(c => c.ConnectionName == connectionName);
            return connection ?? throw new Exception($"Failed to load connection {connectionName}");
        }

        public static void SaveToFile(string filePathFromBase, Connections connections)
        {
            using var writer = new StreamWriter(File.Create(filePathFromBase));
            if (writer is not null)
            {
                string json = JsonConvert.SerializeObject(connections);
                writer.WriteLine(json);
                writer.Close();
            }
        }
    }
}