using System.Web.Http;
using Microsoft.Owin;
using Owin;
using WebApi;
using WebApi.Versioning.Configuration;

[assembly: OwinStartup(typeof(Startup))]

namespace WebApi
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.WithApiVersioning(x =>
                {
                    x.AdvertiseApiVersion(1, "Api v1");
                    x.AdvertiseApiVersion(4, "Api v4");
                    x.AdvertiseApiVersion(8, "Api v8");
                    x.AdvertiseApiVersion(11, "Api v11");
                })
                .AddSwagger();

            app.UseWebApi(config);
        }
    }
}
