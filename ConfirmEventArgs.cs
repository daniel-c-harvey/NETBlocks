
namespace Core
{
    public delegate void ConfirmEventHandler(object sender, ConfirmEventArgs e);
    public class ConfirmEventArgs : EventArgs
    {
        public static ConfirmEventArgs Confirm = new() { IsConfirmed = true };
        public static ConfirmEventArgs Deny = new() { IsConfirmed = false };
        
        public bool IsConfirmed { get; set; } = false;
    }

    //public delegate void ConfirmEventHandler<T>(object sender, ConfirmEventArgs<T> e);
    //public class ConfirmEventArgs<T> : ConfirmEventArgs
    //{
    //    public T NewValue { get; set; }

    //    public ConfirmEventArgs(T newValue)
    //    {
    //        NewValue = newValue;
    //    }
    //}
}
