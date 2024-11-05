
namespace Core
{
    public delegate void ConfirmEventHandler(object sender, ConfirmEventArgs e);
    public class ConfirmEventArgs : EventArgs
    {
        public static ConfirmEventArgs Confirm = new() { IsConfirmed = true };
        public static ConfirmEventArgs Deny = new() { IsConfirmed = false };
        
        public bool IsConfirmed { get; set; } = false;
    }

    public delegate void ResultEventHandler<T>(object sender, ResultEventArgs<T> e);
    public class ResultEventArgs<T>
    {
        public T Result { get; set; }

        public ResultEventArgs(T result)
        {
            Result = result;
        }
    }
}
