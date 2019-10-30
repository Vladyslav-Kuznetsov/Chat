using ChatLib;
using System;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    public class Program
    {
       public static void Main(string[] args)
        {
            try
            {
                var ip = System.Configuration.ConfigurationManager.AppSettings["ip"];
                var port = System.Configuration.ConfigurationManager.AppSettings["port"];
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));

                using (var chatClient = new ChatClient(endPoint))
                {
                    chatClient.Start();
                    chatClient.Write();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
