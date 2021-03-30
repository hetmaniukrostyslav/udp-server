using System.Configuration;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace UDPServer.Application.Infrastructure
{
    public static class ApiConfig
    {
        private const string AddressPath = "Address";
        private const string RouteMapName = "API Default";
        private const string RouteTemplete = "api/{controller}/{id}";
        private const string MediaType = "text/html";

        public static HttpSelfHostConfiguration Configure()
        {
            var address = ConfigurationManager.AppSettings.Get(AddressPath);
            var configuration = new HttpSelfHostConfiguration(address);
            configuration.MapHttpAttributeRoutes();
            configuration.Routes.MapHttpRoute(RouteMapName,
                                                RouteTemplete,
                                                new { id = RouteParameter.Optional });

            configuration.Formatters
                            .JsonFormatter
                            .SupportedMediaTypes
                            .Add(new MediaTypeHeaderValue(MediaType));
            return configuration;
        }
    }
}
