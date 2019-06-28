using System.Web.Http;
using System.Web.Http.Description;
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

            var routeProvider = new DirectRouteProvider();

            config.Services.Replace(
                typeof(IApiExplorer),
                new Versioning.ApiExplorer(config));

            config
                .EnableSwagger(c => c.SingleApiVersion("1", "Api V1"))
                .EnableSwaggerUi();

            config.MapHttpAttributeRoutes(routeProvider);

            app.UseWebApi(config);
        }
    }
}
