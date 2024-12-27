using NetBlocks.Models.Environment;

namespace NetBlocks.Utilities.Environment
{
    public interface IConnectionStringLoader
    {
        Connections? LoadFromFile(string filePathFromBase);
        Connection LoadFromFile(string filePathFromBase, string connectionName);
        void SaveToFile(string filePathFromBase, Connections connections);
    }
}