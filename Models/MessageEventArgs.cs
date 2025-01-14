namespace NetBlocks.Models;

public class MessageEventArgs : EventArgs
{
    public string Message { get; }
    
    public MessageEventArgs(string message)
    {
        Message = message;
    }
}

public delegate void MessageEventHandler(object sender, MessageEventArgs e);