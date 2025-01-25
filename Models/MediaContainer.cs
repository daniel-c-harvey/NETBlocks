using System.Text.RegularExpressions;

namespace NetBlocks.Models;

public class MediaContainer
{
    public MediaBinary Binary { get; set; }
    public string FileName { get; set; }

    public string FileUri
    {
        
    get {
        string uri = Regex.Replace(FileName, @"[.]\w+$", string.Empty);
        uri = Regex.Replace(uri, @"[^\w|\d]", "-");
        uri = System.Web.HttpUtility.UrlEncode(uri); // final catchall for illegal chars
        return uri;
    } 
    }

    public MediaContainer(string name, MediaBinary binary)
    {
        Binary = binary;
        FileName = name;
    }
}