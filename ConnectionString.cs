using Newtonsoft.Json;

namespace Core
{
    public class Connection
    {
        public long ID { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
    }
    public class Connections
    {
        public IEnumerable<Connection> ConnectionStrings { get; set; } = [];
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
            Connection? connection = connections?.ConnectionStrings.FirstOrDefault(c => c.Name == connectionName);            
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