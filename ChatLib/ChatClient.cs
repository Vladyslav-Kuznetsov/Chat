using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ChatLib
{
    public class ChatClient : IDisposable
    {
        private readonly TcpClient _client;
        private readonly IPEndPoint _endPoint;
        private StreamReader _reader;
        private StreamWriter _writer;

        public ChatClient(IPEndPoint endPoint)
            : this(new TcpClient())
        {
            _endPoint = endPoint;
            _client.Connect(endPoint);
        }

        public ChatClient(TcpClient client)
        {
            _client = client;
        }

        public Task Start()
        {
            var stream = _client.GetStream();
            _reader = new StreamReader(stream);
            _writer = new StreamWriter(stream);

            return Task.Run(() => Read());
        }

        private void Read()
        {
            while (true)
            {
                var answer = _reader.ReadLine();
                Console.WriteLine("Answer: " + answer);

                MessageRecivedEvent?.Invoke(this, new ChatMessageEventArgs(answer));
            }
        }

        public void Write()
        {
            while (true)
            {
                //Console.Write("Enter the message: ");
                var msg = Console.ReadLine();
                _writer.WriteLine(msg);
                _writer.Flush();
            }
        }

        public void Send(string message)
        {
            _writer.WriteLine(message);
            _writer.Flush();
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public event EventHandler<ChatMessageEventArgs> MessageRecivedEvent;
    }
}
