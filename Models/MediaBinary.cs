
namespace NetBlocks.Models
{
    public class MediaBinary
    {
        public string Base64 { get; set; } = default!;
        public long Size { get; set; } = default!;
        public string Mime { get; set; } = default!;
        
        // only for serializer
        public MediaBinary() { }
        
        public MediaBinary(MediaBinaryDto other) 
        {
            Base64 = other.Base64;
            Size = other.Size;
            Mime = other.Mime;
        }

        public MediaBinary(byte[] data, long size, string extension)
        {
            Base64 = Convert.ToBase64String(data);
            Size = size;
            Mime = MIME.MimeTypes[extension];
        }
    }
}
