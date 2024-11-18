using System.Reflection;
using System.Text;

namespace NetBlocks.Utilities
{
    public class FileLoader
    {
        public static string LoadResourceFileAsString(Assembly source, string resourceName)
        {
            if (source == null)
            {
                throw new ArgumentException("Assembly not found.");
            }

            // Use the assembly and the resource name to access the resource stream
            using (Stream? stream = source.GetManifestResourceStream(resourceName))
            {
                if (stream is null)
                {
                    throw new ArgumentException($"Resource '{resourceName}' not found. Ensure it is set as an Embedded Resource.");
                }

                // Read the stream into a string and return it
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static List<StreamReader> StreamFilesOfType(string directoryPath, string fileExtension)
        {
            IEnumerable<string> fileNames = GetFilesOfType(directoryPath, fileExtension);
            var files = new List<StreamReader>(fileNames.Count());

            foreach (string name in fileNames)
            {
                files.Add(new StreamReader(File.OpenRead(name)));
            }

            return files;
        }
        public static List<string> GetFilesOfType(string directoryPath, string fileExtension)
        {
            List<string> files = new List<string>();

            try
            {
                // Ensure the file extension starts with a dot.
                if (!fileExtension.StartsWith("."))
                {
                    fileExtension = "." + fileExtension;
                }

                // Verify if the directory exists.
                if (Directory.Exists(directoryPath))
                {
                    // Get all files matching the extension.
                    files.AddRange(Directory.GetFiles(directoryPath, "*" + fileExtension, SearchOption.AllDirectories));
                }
                else
                {
                    Console.WriteLine("Directory does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return files;
        }
    }
}
