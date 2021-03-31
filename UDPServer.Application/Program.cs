using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Configuration;
using System.Reflection;
using System.Web.Http.SelfHost;
using System.Web.Mvc;
using UDPServer.Application.Infrastructure;
using UDPServer.Application.Managers;
using UDPServer.Persistence.Context;
using UDPServer.Persistence.Repositories;

namespace UDPServer.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = ApiConfig.Configure();
            using (var apiServer = new HttpSelfHostServer(configuration))
            {
                IoCConfig.Configure(apiServer.Configuration);

                using (var scope = IoCConfig.Container.BeginLifetimeScope())
                {
                    var  messageListener = scope.Resolve<MessageListener>();  
                    
                    messageListener.Start();
                    apiServer.OpenAsync().Wait();

                    Console.WriteLine($"Server started on: {configuration.BaseAddress}");
                    Console.WriteLine($"Press E to exit!");
                    if (Console.ReadKey().Key == ConsoleKey.E)
                    {
                        messageListener.Stop();
                        apiServer.CloseAsync().Wait();
                    }
                }
            }
        }
    }
}
