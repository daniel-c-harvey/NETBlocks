using System.Text.Encodings.Web;

namespace NetBlocks.Models
{
    public class MediaBinaryDto
    {
        public static MediaBinaryDto From(MediaBinary mediaBinary)
        {
            return new MediaBinaryDto
            {
                Base64 = mediaBinary.Base64,
                Size = mediaBinary.Size,
                Mime = mediaBinary.Mime
            };
        }
        public string Base64 { get; set; }
        public long Size { get; set; }
        public string Mime { get; set; }
    }
}
