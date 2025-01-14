﻿
namespace NetBlocks.Models
{
    public class MediaBinary
    {
        public string Base64 { get; set; } = default!;
        public long Size { get; set; } = default!;
        public string Extension { get; set; } = default!;
        
        // only for serializer
        public MediaBinary() { }
        
        public MediaBinary(MediaBinaryDto other) 
        {
            Base64 = Convert.ToBase64String(other.Bytes); // probably smart to make the API operate in base64 instead of byte arrays, it's much more compressed
            Size = other.Size;
            Extension = other.Extension;
        }

        public MediaBinary(byte[] data, long size, string extension)
        {
            Base64 = Convert.ToBase64String(data);
            Size = size;
            Extension = extension;
        }
    }
}
