namespace NetBlocks.Models
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
