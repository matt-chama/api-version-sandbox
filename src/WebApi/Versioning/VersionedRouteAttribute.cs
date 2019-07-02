using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Routing;
using System.Web.Http.Routing.Constraints;

namespace WebApi.Versioning
{
    public class VersionedRouteAttribute : Attribute, IActionFilter, IDirectRouteFactory
    {
        public int MinimumValue { get; }
        public int MaximumValue { get; }

        public VersionedRouteAttribute(
            string template,
            int minimumValue,
            int maximumValue)
        {
            Template = template;
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
        }

        public string Template { get; }

        public string Name { get; set; }

        public int Order { get; set; }

        public Dictionary<string, object> Defaults { get; set; }

        public Dictionary<string, object> DataTokens { get; set; }

        public Dictionary<string, object> Constraints { get; set; }

        public RouteEntry CreateRoute(DirectRouteFactoryContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // get the prefix
            var routePrefixAttributes = context
                .Actions
                .First()
                .ControllerDescriptor
                .GetCustomAttributes<RoutePrefixAttribute>();

            string prefix = null;

            if (routePrefixAttributes != null && routePrefixAttributes.Any())
            {
                if (routePrefixAttributes.Count > 1)
                {
                    throw new Exception("Multiple route prefix attributes are not allowed.");
                }

                prefix = routePrefixAttributes.Single().Prefix;
            }

            string[] templateSplit = Template.Split('/');

            string constraintName = $"v_{MinimumValue}_{MaximumValue}";

            List<string> routeSegments = new List<string>
            {
                "api",
                $"v{{{constraintName}:int:min({MinimumValue}):max({MaximumValue})}}",
                prefix
            };

            Constraints = new Dictionary<string, object>
            {
                {
                    constraintName,
                    new CompoundRouteConstraint(new List<IHttpRouteConstraint>
                    {
                        new IntRouteConstraint(),
                        new MinRouteConstraint(MinimumValue),
                        new MaxRouteConstraint(MaximumValue)
                    })
                }
            };

            routeSegments.AddRange(templateSplit);

            string newTemplate = string.Join("/", routeSegments);

            IDirectRouteBuilder builder = context.CreateBuilder(newTemplate);

            builder.Name = Name;
            builder.Order = Order;

            IDictionary<string, object> defaults1 = builder.Defaults;

            if (defaults1 == null)
            {
                builder.Defaults = Defaults;
            }
            else
            {
                IDictionary<string, object> defaults2 = Defaults;
                if (defaults2 != null)
                {
                    foreach (KeyValuePair<string, object> keyValuePair in defaults2)
                        defaults1[keyValuePair.Key] = keyValuePair.Value;
                }
            }

            IDictionary<string, object> constraints1 = builder.Constraints;

            if (constraints1 == null)
            {
                builder.Constraints = Constraints;
            }
            else
            {
                IDictionary<string, object> constraints2 = Constraints;
                if (constraints2 != null)
                {
                    foreach (KeyValuePair<string, object> keyValuePair in constraints2)
                        constraints1[keyValuePair.Key] = keyValuePair.Value;
                }
            }

            IDictionary<string, object> dataTokens1 = builder.DataTokens;
            if (dataTokens1 == null)
            {
                builder.DataTokens = DataTokens;
            }
            else
            {
                IDictionary<string, object> dataTokens2 = DataTokens;
                if (dataTokens2 != null)
                {
                    foreach (KeyValuePair<string, object> keyValuePair in dataTokens2)
                        dataTokens1[keyValuePair.Key] = keyValuePair.Value;
                }
            }

            var result = builder.Build();

            return result;
        }

        public bool AllowMultiple { get; set; }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            bool versionExists = actionContext
                .ControllerContext
                .RouteData
                .Values
                .Any(x => x.Key.StartsWith("v_"));

            if (!versionExists)
            {
                throw new Exception("This route requires a version value but none was provided.");
            }

            string versionString = actionContext
                .ControllerContext
                .RouteData
                .Values
                .First(x => x.Key.StartsWith("v_"))
                .Value as string;

            if (string.IsNullOrEmpty(versionString) ||
                !int.TryParse(versionString, out int versionInt))
            {
                throw new Exception("This route requires a version value but none was provided.");
            }

            if (versionInt < MinimumValue)
            {
                return new HttpResponseMessage(HttpStatusCode.Gone);
            }

            if (versionInt > MaximumValue)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return await continuation();
        }
    }
}
