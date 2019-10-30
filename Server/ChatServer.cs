using ChatLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Server
{
   public class ChatServer : IDisposable
    {
        private readonly TcpListener _server;
        private readonly List<ChatClient> _clients = new List<ChatClient>();

        public ChatServer(int port)
        {
            _server = new TcpListener(IPAddress.Any, port);
            Console.WriteLine("Start Server on port " + port);
            _server.Start();
        }

        public void Start()
        {
            while (true)
            {
                var tcpClient = _server.AcceptTcpClient();
                var chatClient = new ChatClient(tcpClient);
                Console.WriteLine("Client was connected. " + tcpClient.Client.RemoteEndPoint);

                _clients.Add(chatClient);
                chatClient.Start();
                chatClient.MessageRecivedEvent += ChatClient_MessageRecivedEvent;
            }
        }

        private void ChatClient_MessageRecivedEvent(object sender, ChatMessageEventArgs e)
        {
            foreach (var client in _clients.Where(x => sender != x))
                client.Send(e.Message);
        }

        public void Dispose()
        {
            foreach (var item in _clients)
            {
                item.Dispose();
            }
            _server.Stop();
        }
    }
}
