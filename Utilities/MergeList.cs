using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlocks.Utilities
{
    public class MergeList<T> : IList<T>
    {
        private IList<T> _list;

        public int Count => _list.Count;

        public bool IsReadOnly => _list.IsReadOnly;

        public T this[int index] { get => _list[index]; set => _list[index] = value; }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }


        public void Add(T item)
        {
            _list.Add(item);
        }

        public void Merge(IList<T> list)
        {
            foreach (var item in list) // todo finish this
            {
                if (!_list.Contains(item))
                {
                    _list.Add(item);
                }
            }
        }



        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }


        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }


        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}

