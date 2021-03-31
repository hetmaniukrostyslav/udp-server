using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPClient
{
    public class UdpClientManager
    {
        private readonly UdpClient _client;

        public UdpClientManager(string hostname, int port)
        {
            _client = new UdpClient();
            _client.Connect(hostname, port);
        }

        public void Send(string message)
        {
            var datagram = Encoding.ASCII.GetBytes(message);
            _client.Send(datagram, datagram.Length);
        }
    }
}
