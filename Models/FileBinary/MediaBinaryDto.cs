using NetBlocks.Models.FileBinary.Factory;

namespace NetBlocks.Models.FileBinary
{
    public abstract class MediaBinaryDto<TMedia, TDto, TParams>
    where TMedia : MediaBinary<TMedia, TDto, TParams>
    where TDto : MediaBinaryDto<TMedia, TDto, TParams>
    where TParams : MediaBinaryParams
    {
        public virtual void LoadFromMedia(TMedia media)
        {
            Base64 = media.Base64;
            Size = media.Size;
            Mime = media.Mime;
        }
        
        public string Base64 { get; set; }
        public long Size { get; set; }
        public string Mime { get; set; }
    }
}
