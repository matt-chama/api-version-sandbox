using System;
using System.Web.Http;

namespace WebApi.Versioning.Configuration
{
    public static class HttpConfigurationExtensions
    {
        public static VersionedHttpConfiguration WithApiVersioning(
            this HttpConfiguration httpConfiguration,
            Action<ApiVersionConfiguration> configureApiVersion)
        {
            ApiVersionConfiguration versionConfiguration =
                new ApiVersionConfiguration();
            configureApiVersion(versionConfiguration);

            var routeProvider = new DirectRouteProvider("api");
            httpConfiguration.MapHttpAttributeRoutes(routeProvider);

            return new VersionedHttpConfiguration(
                httpConfiguration,
                versionConfiguration);
        }
    }
}