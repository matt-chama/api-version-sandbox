using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace WebApi.Tests
{
    public class TestWebApiResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            return new List<Assembly> { typeof(WebApi.Controllers.VersionedController).Assembly };
        }
    }
}