namespace NetBlocks.Utilities;

public class DisplayEnumeration<T> : Enumeration<T> where T : DisplayEnumeration<T>
{
    public string DisplayName { get; init; }
    
    protected DisplayEnumeration(int id, string name, string displayName) : base(id, name)
    {
        DisplayName = displayName;
    }
}