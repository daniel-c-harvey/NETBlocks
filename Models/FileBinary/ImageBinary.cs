namespace NetBlocks.Models.FileBinary;

public class ImageBinaryParams : MediaBinaryParams
{
    public required double AspectRatio { get; set; }
}

public class ImageBinary : MediaBinary<ImageBinary, ImageBinaryDto, ImageBinaryParams>
{
    public double AspectRatio { get; set; }
    
    public ImageBinary() { }
        
    public override void LoadFromDto(ImageBinaryDto other) 
    {
        base.LoadFromDto(other);
        AspectRatio = other.AspectRatio;
    }

    public override void LoadFromParameters(ImageBinaryParams parameters) 
    {
        base.LoadFromParameters(parameters);
        AspectRatio = parameters.AspectRatio;
    }
    
}