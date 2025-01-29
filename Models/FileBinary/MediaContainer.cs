using System.Text.RegularExpressions;

namespace NetBlocks.Models.FileBinary;

public class MediaContainer<TMedia, TDto, TParams>
where TMedia : MediaBinary<TMedia, TDto, TParams>, new()
where TDto : MediaBinaryDto<TMedia, TDto, TParams>
where TParams : MediaBinaryParams
{
    public TMedia Binary { get; set; }
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

    public MediaContainer(string name, TMedia binary)
    {
        Binary = binary;
        FileName = name;
    }
}