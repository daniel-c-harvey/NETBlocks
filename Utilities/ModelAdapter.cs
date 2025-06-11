using System.Reflection;

namespace NetBlocks.Utilities;

public interface IModelAdapter<TSource, TDestination>
{
    TDestination Adapt(TSource source);
}

public class ModelAdapter<TSource, TDestination, TBase> : IModelAdapter<TSource, TDestination>
    where TSource : TBase
    where TDestination : TBase, new()
{
    public TDestination Adapt(TSource source)
    {
        var destination = new TDestination();
        // Use reflection to map common properties from base class and derived types
        PropertyInfo[] sourceProperties = typeof(TSource).GetProperties();
        PropertyInfo[] destProperties = typeof(TDestination).GetProperties();
        
        foreach (var sourceProp in sourceProperties)
        {
            var destProp = destProperties.FirstOrDefault(p => p.Name == sourceProp.Name && 
                                                              p.PropertyType == sourceProp.PropertyType);
            if (destProp != null && destProp.CanWrite)
            {
                destProp.SetValue(destination, sourceProp.GetValue(source));
            }
        }
        
        return destination;
    }
}