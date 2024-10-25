
namespace Core
{
    public class Page
    {
        public int PageIndex { get; private set; }
        public int PageNumber => PageIndex + 1;
        public int PageLength { get; private set; }

        public Page(int index, int length)
        {
            PageIndex = index;
            PageLength = length;
        }


    }
}
