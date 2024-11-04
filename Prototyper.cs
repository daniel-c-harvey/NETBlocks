using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlocks
{
    public static class Prototyper
    {
        /// <summary>
        /// Create prototype objects (default initialized) for all derived types of the specified Type.
        /// </summary>
        public static IEnumerable<T> PrototypeDerivedTypes<T>()
        where T : ICloneable<T>
        {
            Type baseType = typeof(T);
            return baseType.Assembly.GetTypes()
                                    .Where(t => t.IsSubclassOf(baseType) && !t.IsAbstract)
                                    .Select(t => (T?)Activator.CreateInstance(t) 
                                                 ?? throw new Exception("Failed to create Prototypes"));

        }
    }
}
