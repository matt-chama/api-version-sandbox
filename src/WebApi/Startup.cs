using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.Owin;
using Owin;
using WebApi;
using WebApi.Versioning;

[assembly: OwinStartup(typeof(Startup))]

namespace WebApi
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            var routeProvider = new VersionedDirectRouteProvider();

            config.MapHttpAttributeRoutes(routeProvider);

            app.UseWebApi(config);
        }
    }
}
