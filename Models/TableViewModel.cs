using NetBlocks.Interfaces;
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
        bool IsDirty { get; }
    }

    public class TableViewModel<T> : IIndexed, IEditable, IValue<T>
    where T : ICloneable<T>
    {
        private T _value = default!;
        private T _backup = default!;
        private bool _editable = false;

        public int Index { get; }
        public bool IsEditable
        {
            get => _editable || IsNew || IsDirty;
            set
            {
                if (value)
                {
                    Backup();
                }
                else
                {
                    Restore();
                }
                _editable = value;
            }
        }
        public bool IsNew { get; set; } = false;
        public bool IsDirty => !_backup.Equals(_value);
        public T Value 
        {
            get => _value;
            set
            {
                if (value != null && !value.Equals(_value))
                {
                    _value = value;
                    _backup = value;
                }
            }
        }

        public void Backup()
        {
            _backup = _value.Clone();
        }

        public void Commit()
        {
            Backup();
            _editable = false;
            IsNew = false;
        }

        public void Restore()
        {
            _value = _backup;
        }

        public TableViewModel(T value, int index)
        {
            Value = value;
            Index = index;
        }


    }
}
