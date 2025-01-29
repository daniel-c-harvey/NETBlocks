namespace NetBlocks.Models.FileBinary.Factory
{
    public static class MediaFactory
    {
        public static TMedia CreateFromDto<TMedia, TDto, TParams>(TDto dto)
        where TMedia : MediaBinary<TMedia, TDto, TParams>, new()
        where TDto : MediaBinaryDto<TMedia, TDto, TParams>
        where TParams : MediaBinaryParams
        {
            TMedia media = new TMedia();
            media.LoadFromDto(dto);
            return media;
        }

        public static TMedia CreateFromParams<TMedia, TDto, TParams>(TParams parameters)
        where TMedia : MediaBinary<TMedia, TDto, TParams>, new()
        where TDto : MediaBinaryDto<TMedia, TDto, TParams>
        where TParams : MediaBinaryParams
        {
            TMedia media = new TMedia();
            media.LoadFromParameters(parameters);
            return media;
        }
    }
    
    public static class DtoFactory
    {
        public static TDto CreateFromMedia<TMedia, TDto, TParams>(TMedia media)
            where TMedia : MediaBinary<TMedia, TDto, TParams>
            where TDto : MediaBinaryDto<TMedia, TDto, TParams>, new()
            where TParams : MediaBinaryParams
        {
            TDto dto = new();
            dto.LoadFromMedia(media);
            return dto;
        }
    }
}