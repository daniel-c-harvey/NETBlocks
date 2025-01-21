namespace NetBlocks.Utilities;

public static class EnumExtensions
{
    public static T Merge<T>(this T me, T other) where T : struct, Enum
    {
        return new[] { me, other }.Max();
    }
    //
    // public static T GetHighestPriority<T>(this IEnumerable<T> values) where T : struct, Enum
    // {
    //     if (values == null || values.Any())
    //         throw new ArgumentException("Values collection cannot be null or empty");
    //
    //     // Return the maximum value as enums are ordered by their underlying value
    //     return values.Max();
    // }
}