using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using UDPServer.Application.Managers;
using UDPServer.Persistence.Context;
using UDPServer.Persistence.Models;
using UDPServer.Persistence.Repositories;

namespace UDPServer.Application.Infrastructure
{
    public static class IoCConfig
    {
        public static IContainer Container;

        public static void Configure(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ApplicationDbContext>();
            builder.RegisterType<MessageRepository>();
            builder.RegisterType<SenderRepository>();
            builder.RegisterType<MessageListener>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;
            Container = container;
        }
    }
}
