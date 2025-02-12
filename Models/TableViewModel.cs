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

    public interface IEditable
    {
        bool IsNew { get; set; }
        bool IsEditable { get; set; }
        bool IsDirty { get; set; }
    }

    public class TableViewModel<T> : IIndexed, IEditable, IValue<T>
    {
        public int Index { get; }
        public bool IsEditable { get; set; } = false;
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
