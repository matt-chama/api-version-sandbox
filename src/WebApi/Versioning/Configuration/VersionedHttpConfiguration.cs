using System.Linq;
using System.Web.Http;
using Swashbuckle.Application;

namespace WebApi.Versioning.Configuration
{
    public class VersionedHttpConfiguration
    {
        private readonly HttpConfiguration _httpConfiguration;
        private readonly ApiVersionConfiguration _versionConfiguration;

        public VersionedHttpConfiguration(
            HttpConfiguration httpConfiguration,
            ApiVersionConfiguration versionConfiguration)
        {
            _httpConfiguration = httpConfiguration;
            _versionConfiguration = versionConfiguration;
        }

        public void AddSwagger()
        {
            _httpConfiguration.EnableSwagger(c =>
                    c.MultipleApiVersions((description, version) =>
                        {
                            int versionInt = int.Parse(version);
                            var versionAttribute = description
                                .ActionDescriptor
                                .GetCustomAttributes<VersionedRouteAttribute>()
                                .SingleOrDefault();

                            if (versionAttribute == default)
                            {
                                return true;
                            }

                            return versionInt >= versionAttribute.MinimumValue &&
                                   versionInt <= versionAttribute.MaximumValue;
                        },
                        versionConfig =>
                        {
                            foreach (var apiVersion in _versionConfiguration.Versions)
                            {
                                versionConfig.Version(
                                    apiVersion.Version.ToString(),
                                    apiVersion.Description);
                            }
                        }))
                .EnableSwaggerUi();
        }
    }
}