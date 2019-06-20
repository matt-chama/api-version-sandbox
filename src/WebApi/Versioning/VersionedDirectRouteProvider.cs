using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace WebApi.Versioning
{
    public class VersionedDirectRouteProvider : DefaultDirectRouteProvider
    {
        protected override string GetRoutePrefix(
            HttpControllerDescriptor controllerDescriptor)
        {
            return null;
        }
    }
}