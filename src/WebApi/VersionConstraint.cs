using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Web.Http.Routing.Constraints;

namespace WebApi
{
    public class VersionConstraint : IHttpRouteConstraint
    {
        private readonly RangeRouteConstraint _innerConstraint;

        public VersionConstraint(int min, int max)
        {
            _innerConstraint = new RangeRouteConstraint(min, max);
        }

        public bool Match(
            HttpRequestMessage request,
            IHttpRoute route,
            string parameterName,
            IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            var split = request.RequestUri.AbsolutePath.Split('/');

            var versionString = split[1].Substring(1);
            var versionInt = int.Parse(versionString);

            return versionInt >= _innerConstraint.Min &&
                   versionInt <= _innerConstraint.Max;
        }
    }
}