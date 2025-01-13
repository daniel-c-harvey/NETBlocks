﻿namespace NetBlocks.Models
{
    public class MediaBinaryDto
    {
        public byte[] Bytes { get; set; } = default!;
        public long Size { get; set; } = default!;
        public string Extension { get; set; } = default!;
    }
}