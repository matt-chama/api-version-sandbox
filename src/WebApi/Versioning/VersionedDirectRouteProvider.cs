using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace WebApi.Versioning
{
    public class VersionedDirectRouteProvider : DefaultDirectRouteProvider
    {
        private HttpActionDescriptor _actionContext;

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
                return base.GetRoutePrefix(controllerDescriptor);
            }

            if (_actionContext is ReflectedHttpActionDescriptor reflectedAction)
            {
                var attribute = reflectedAction
                    .MethodInfo
                    .GetCustomAttribute(typeof(VersionedRouteAttribute));

                if (attribute == null)
                {
                    return base.GetRoutePrefix(controllerDescriptor);
                }
            }

            return null;
        }
    }
}
