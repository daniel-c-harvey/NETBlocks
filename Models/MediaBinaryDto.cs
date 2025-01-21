using System.Text.Encodings.Web;

namespace NetBlocks.Models
{
    public class MediaBinaryDto
    {
        public static MediaBinaryDto From(MediaBinary mediaBinary)
        {
            return new MediaBinaryDto
            {
                Bytes = Convert.FromBase64String(mediaBinary.Base64),
                Size = mediaBinary.Size,
                Mime = MIME.MimeTypes[mediaBinary.Extension]
            };
        }
        public byte[] Bytes { get; set; }
        public long Size { get; set; }
        public string Mime { get; set; }
    }
}
