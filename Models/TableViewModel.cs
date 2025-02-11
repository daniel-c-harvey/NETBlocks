using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlocks.Models
{
    public interface IValue<T>
    {
        T Value { get; }
    }

    public interface IIndexed
    {
        int Index { get; }
    }

    public interface INew
    {
        bool IsNew { get; set; }
    }
    
    public interface IDirty
    {
        bool IsDirty { get; set; }
    }

    public class TableViewModel<T> : IIndexed, INew, IDirty, IValue<T>
    {
        public int Index { get; }
        public bool IsNew { get; set; } = false;
        public bool IsDirty { get; set; } = false;
        public T Value { get; set; }

        public TableViewModel(T value, int index)
        {
            Value = value;
            Index = index;
        }
    }
}
