using System.Web.Http;
using System.Web.Http.Dispatcher;
using Owin;
using WebApi.Versioning;

namespace WebApi.Tests
{
    public class OwinTestConfiguration
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.Services.Replace(typeof(IAssembliesResolver), new TestWebApiResolver());

            var routeProvider = new DirectRouteProvider();
            config.MapHttpAttributeRoutes(routeProvider);

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            app.UseWebApi(config);
        }
    }
}