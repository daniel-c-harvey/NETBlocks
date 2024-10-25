using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class RunType<TSuperElement>
    {
        Type DerivedType { get; }
        TSuperElement Value { get; }

        public RunType(Type type, TSuperElement value)
        {
            DerivedType = type;
            Value = value;
        }
    }
}
