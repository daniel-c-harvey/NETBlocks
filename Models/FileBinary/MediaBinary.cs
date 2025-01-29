
namespace NetBlocks.Models.FileBinary
{
    public class MediaBinaryParams
    {
        public required byte[] Data { get; set; }
        public required long Size { get; set; }
        public required string Extension { get; set; }
    }
    
    public abstract class MediaBinary<TSelf, TDto, TParams>
    where TSelf : MediaBinary<TSelf, TDto, TParams>
    where TDto : MediaBinaryDto<TSelf, TDto, TParams>
    where TParams : MediaBinaryParams
    {
        public string Base64 { get; set; } = default!;
        public long Size { get; set; } = default!;
        public string Mime { get; set; } = default!;
        
        public MediaBinary() { }
        
        public virtual void LoadFromDto(TDto other) 
        {
            Base64 = other.Base64;
            Size = other.Size;
            Mime = other.Mime;
        }

        public virtual void LoadFromParameters(TParams parameters)
        {
            Base64 = Convert.ToBase64String(parameters.Data);
            Size = parameters.Size;
            Mime = MIME.MimeTypes[parameters.Extension];
        }
    }
}
