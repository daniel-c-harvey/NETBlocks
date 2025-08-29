namespace NetBlocks.Utilities;

public class NTree<T>
{
    public List<Node> Roots { get; private set; }
    
    public class Node
    {
        public T? Value { get; set; }
        public List<Node> Children { get; private set; } = [];
    }

    public NTree()
    {
        Roots = [];
    }
    
    public NTree(List<Node> roots)
    {
        Roots = roots;
    }
}