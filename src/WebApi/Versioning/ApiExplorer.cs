using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing;
using System.Web.Http.Routing.Constraints;

namespace WebApi.Versioning
{
    public class ApiExplorer : IApiExplorer
    {
        private readonly HttpConfiguration _config;

        private readonly Lazy<Collection<ApiDescription>> _descriptionLoader;

        public Collection<ApiDescription> ApiDescriptions => _descriptionLoader.Value;

        public ApiExplorer(HttpConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));

            _descriptionLoader = new Lazy<Collection<ApiDescription>>(GetApiDescriptions);
        }

        private Collection<ApiDescription> GetApiDescriptions()
        {
            _config.EnsureInitialized();

            var flattenedRoutes = FlattenRoutes(_config.Routes);
            var versions = GetApiVersions(flattenedRoutes);

            var result = new Collection<ApiDescription>();

            var controllerSelector = _config.Services.GetHttpControllerSelector();
            var controllerMappings = controllerSelector.GetControllerMapping();

            if (controllerMappings == null)
            {
                return result;
            }

            foreach (int version in versions)
            {
                foreach (IHttpRoute route in flattenedRoutes)
                {
                }
            }

            return result;
        }

        private static IEnumerable<int> GetApiVersions(
            IEnumerable<IHttpRoute> routes)
        {
            HashSet<int> result = new HashSet<int>();

            foreach (var route in routes)
            {
                Debug.WriteLine($"Examining route {route.RouteTemplate}");

                if (route.Constraints == null || !route.Constraints.Any())
                {
                    continue;
                }

                bool constraintFound = route
                    .Constraints
                    .TryGetValue("version", out object constraint);

                if(!constraintFound || !(constraint is CompoundRouteConstraint))
                {
                    continue;
                }

                var versionConstraint = (CompoundRouteConstraint) constraint;

                var minConstraint = versionConstraint
                    .Constraints
                    .OfType<MinRouteConstraint>()
                    .First();

                var maxConstraint = versionConstraint
                    .Constraints
                    .OfType<MaxRouteConstraint>()
                    .First();

                var min = (int) minConstraint.Min;
                var max = (int) maxConstraint.Max;

                foreach (int i in Enumerable.Range(min, max - min))
                {
                    result.Add(i);
                }
            }

            return result;
        }

        static IEnumerable<IHttpRoute> FlattenRoutes(IEnumerable<IHttpRoute> routes)
        {
            foreach (var route in routes)
            {
                if (route is IEnumerable<IHttpRoute> nested)
                {
                    foreach (var subRoute in FlattenRoutes(nested))
                    {
                        yield return subRoute;
                    }
                }
                else
                {
                    yield return route;
                }
            }
        }
    }
}
