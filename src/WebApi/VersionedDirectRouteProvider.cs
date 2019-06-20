using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace WebApi
{
    public class VersionedDirectRouteProvider : DefaultDirectRouteProvider
    {
        public override IReadOnlyList<RouteEntry> GetDirectRoutes(
            HttpControllerDescriptor controllerDescriptor,
            IReadOnlyList<HttpActionDescriptor> actionDescriptors,
            IInlineConstraintResolver constraintResolver)
        {
            var result = base.GetDirectRoutes(controllerDescriptor, actionDescriptors, constraintResolver);

            return result;
        }

        protected override IReadOnlyList<IDirectRouteFactory> GetControllerRouteFactories(
            HttpControllerDescriptor controllerDescriptor)
        {
            var result = base.GetControllerRouteFactories(controllerDescriptor);

            return result;
        }

        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(
            HttpActionDescriptor actionDescriptor)
        {
            var result = base.GetActionRouteFactories(actionDescriptor);

            return result;
        }

        protected override IReadOnlyList<RouteEntry> GetControllerDirectRoutes(
            HttpControllerDescriptor controllerDescriptor,
            IReadOnlyList<HttpActionDescriptor> actionDescriptors,
            IReadOnlyList<IDirectRouteFactory> factories,
            IInlineConstraintResolver constraintResolver)
        {
            var result = base.GetControllerDirectRoutes(
                controllerDescriptor,
                actionDescriptors,
                factories,
                constraintResolver);

            return result;
        }

        protected override IReadOnlyList<RouteEntry> GetActionDirectRoutes(
            HttpActionDescriptor actionDescriptor,
            IReadOnlyList<IDirectRouteFactory> factories,
            IInlineConstraintResolver constraintResolver)
        {
            var result = base.GetActionDirectRoutes(
                actionDescriptor,
                factories,
                constraintResolver);

            return result;
        }

        protected override string GetRoutePrefix(
            HttpControllerDescriptor controllerDescriptor)
        {
            return null;
        }
    }
}