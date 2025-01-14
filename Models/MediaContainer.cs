namespace NetBlocks.Models;

public class MediaContainer
{
    public MediaBinary Binary { get; set; }
    public string FileName { get; set; }
    public string FileUri => System.Web.HttpUtility.UrlEncode(FileName);

    public MediaContainer(string name, MediaBinary binary)
    {
        Binary = binary;
        FileName = name;
    }
}