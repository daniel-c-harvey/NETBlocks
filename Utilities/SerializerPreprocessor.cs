using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NetBlocks.Utilities
{
    public class SerializableDerivationsAttribute : Attribute 
    {
        public SerializableDerivationsAttribute()
        {
            
        }
    }

    //public static class SerializerPreprocessor
    //{
    //    static SerializerPreprocessor()
    //    {
    //        var ass = Assembly.GetEntryAssembly();

    //        while (ass != null)
    //        {
    //            foreach (Type t in ass.GetTypes())
    //            {
    //                if (t.GetCustomAttributesData().Any(cad => cad.AttributeType == typeof(SerializableDerivationsAttribute)))
    //                {
    //                    //t.
    //                }
    //            }
    //        }
            
    //    }


    //}
}
