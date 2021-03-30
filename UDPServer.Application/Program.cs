using System;
using System.Web.Http.SelfHost;
using UDPServer.Application.Infrastructure;

namespace UDPServer.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var server = new HttpSelfHostServer(ApiConfig.Configure()))
            {
                server.OpenAsync().Wait();
                Console.ReadKey();
            }
        }
    }
}
