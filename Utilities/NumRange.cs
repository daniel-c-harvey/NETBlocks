using System.Collections;
using System.Numerics;


public class NumRange<T> : IEnumerable<T>  where T : IBinaryInteger<T>
{
    public List<T> Range { get; }
    public NumRange(T start, T last)
    {
        T range = last - start + T.One;

        if (range.CompareTo(0) < 0)
            throw new ArgumentException("The bounds of the range are not valid.");

        Range = new List<T>();

        for (T count = start; count <= last; count++)
            Range.Add(count);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Range.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}