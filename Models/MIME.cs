﻿namespace NetBlocks.Models
{
    public static class MIME
    {
        public static readonly Dictionary<string, string> MimeTypes = new() 
        {
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png", "image/png" },
            { ".gif", "image/gif" },
            { ".webp", "image/webp" },
            { ".svg", "image/svg+xml" },
            { ".bmp", "image/bmp" }
        };

        public static readonly Dictionary<string, string> Extensions = new()
        {
            { "image/jpeg", ".jpg" },
            { "image/png", ".png" },
            { "image/gif", ".gif" },
            { "image/webp", ".webp" },
            { "image/svg+xml", ".svg" },
            { "image/bmp", ".bmp" }
        };
    }
}
