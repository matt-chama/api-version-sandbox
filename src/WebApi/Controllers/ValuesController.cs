using System.Web.Http;
using WebApi.Versioning;

namespace WebApi.Controllers
{
    [RoutePrefix("versioned")]
    public class VersionedController : ApiController
    {
        [HttpGet, VersionedRoute("get", 4, 6)]
        public IHttpActionResult Get()
        {
            return Ok(new[] { "value1", "value2" });
        }

        [HttpGet, VersionedRoute("get", 7, 10)]
        public IHttpActionResult Get2()
        {
            return Ok(new[] { "get 2 value1", "get 2 value2" });
        }
    }
}
