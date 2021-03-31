using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Reflection;
using System.Web.Http.SelfHost;
using System.Web.Mvc;
using UDPServer.Application.Infrastructure;
using UDPServer.Persistence.Context;
using UDPServer.Persistence.Repositories;

namespace UDPServer.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var server = new HttpSelfHostServer(ApiConfig.Configure()))
            {
                IoCConfig.Configure(server.Configuration);

                server.OpenAsync().Wait();
                Console.ReadKey();
            }
        }
    }
}
