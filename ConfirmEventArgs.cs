
namespace NetBlocks
{
    public delegate void ConfirmEventHandler<T>(object sender, ConfirmEventArgs<T> e);

    public class ConfirmEventArgs<T> : EventArgs
    {
        public T NewValue { get; set; }
        public bool Confirm { get; set; } = false;

        public ConfirmEventArgs(T newValue)
        {
            NewValue = newValue;
        }
    }
}
