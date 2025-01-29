namespace NetBlocks.Models.FileBinary;

public class ImageBinaryDto : MediaBinaryDto<ImageBinary, ImageBinaryDto, ImageBinaryParams>
{
    public override void LoadFromMedia(ImageBinary imageBinary)
    {
        base.LoadFromMedia(imageBinary);
        AspectRatio = imageBinary.AspectRatio;
    }
    public double AspectRatio { get; set; }
}