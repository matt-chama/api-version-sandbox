using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace WebApi.Versioning
{
    public class DirectRouteProvider : DefaultDirectRouteProvider
    {
        private readonly string _globalPrefix;
        private HttpActionDescriptor _actionContext;

        public DirectRouteProvider(string globalPrefix)
        {
            _globalPrefix = globalPrefix;
        }

        protected override IReadOnlyList<RouteEntry> GetActionDirectRoutes(
            HttpActionDescriptor actionDescriptor,
            IReadOnlyList<IDirectRouteFactory> factories,
            IInlineConstraintResolver constraintResolver)
        {
            _actionContext = actionDescriptor;

            var result = base.GetActionDirectRoutes(
                actionDescriptor,
                factories,
                constraintResolver);

            _actionContext = null;

            return result;
        }

        protected override string GetRoutePrefix(
            HttpControllerDescriptor controllerDescriptor)
        {
            if (_actionContext == null)
            {
                return string.Join("/", _globalPrefix, base.GetRoutePrefix(controllerDescriptor));
            }

            if (_actionContext is ReflectedHttpActionDescriptor reflectedAction)
            {
                var attribute = reflectedAction
                    .MethodInfo
                    .GetCustomAttribute(typeof(VersionedRouteAttribute));

                if (attribute == null)
                {
                    return string.Join("/", _globalPrefix, base.GetRoutePrefix(controllerDescriptor));
                }
            }

            return null;
        }
    }
}
