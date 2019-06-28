using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Swashbuckle.Application;
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

            config.EnableSwagger(c=>c.SingleApiVersion("1", "Api V1"))
                .EnableSwaggerUi();

            config.MapHttpAttributeRoutes(routeProvider);

            app.UseWebApi(config);
        }
    }
}
