
using System.Text;

namespace NetBlocks.Utilities
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Executes a method on every element in the enumerable.
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> oEnumerable, Action<T> pfFunc)
        {
            foreach (T o in oEnumerable)
                pfFunc(o);
        }

        /// <summary>
        /// Executes a method on every element in the enumerable.
        /// </summary>
        public static TRet ForEach<TRet, T>(this IEnumerable<T> oEnumerable, TRet ret, Action<TRet, T> pfFunc)
        {
            foreach (T o in oEnumerable)
                pfFunc(ret, o);
            return ret;
        }

        public static IEnumerable<Counted<TEntity>> ZipCounted<TEntity>(this IEnumerable<TEntity> enumerable)
        {
            return enumerable.Zip(Enumerable.Range(1, enumerable.Count())).Select(t => new Counted<TEntity> { Entity = t.First, Ordinal = t.Second });
        }
        
        public static IEnumerable<Selectable<TEntity>> ZipSelectable<TEntity>(this IEnumerable<TEntity> enumerable)
        {
            return enumerable.Select(t => new Selectable<TEntity> { Entity = t, Selected = t?.Equals(enumerable.FirstOrDefault()) ?? false });
        }

        public static IEnumerable<TEntity> Apply<TEntity>(this IEnumerable<TEntity> enumerable, Action<TEntity> action)
        {
            foreach (TEntity entity in enumerable)
            {
                action(entity);
            }

            return enumerable;
        }

        public static IEnumerable<T2> ListFor<T, T2>(this IEnumerable<T> range, Func<T, T2> action)
        {
            List<T2> list = new List<T2>();
            foreach (T entity in range)
            {
                list.Add(action(entity));
            }

            return list;
        }
    }

    public static class StringBuilderExtensions
    {
        public static string Substring(this StringBuilder sb, int startIndex, int endIndex)
        {
            StringBuilder result = new(endIndex - startIndex);

            for (int i = startIndex; i < endIndex; i++)
            {
                result.Insert(i - startIndex, sb[i]);
            }

            return result.ToString();
        }
    }

    public class Counted<T>
    {
        public int Ordinal { get; set; }
        public T Entity { get; set; } = default!;
    }
    
    public class Selectable<T>
    {
        public bool Selected { get; set; } = default;
        public T Entity { get; set; } = default!;
    }
}