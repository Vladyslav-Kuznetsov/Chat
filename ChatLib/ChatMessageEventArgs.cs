using System;

namespace ChatLib
{
    public class ChatMessageEventArgs : EventArgs
    {
        public ChatMessageEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
