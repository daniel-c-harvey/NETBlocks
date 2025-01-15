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
                Extension = mediaBinary.Extension
            };
        }
        public required byte[] Bytes { get; set; }
        public required long Size { get; set; }
        public required string Extension { get; set; }
    }
}
