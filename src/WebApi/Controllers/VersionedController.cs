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
            return Ok(new { HelloFrom = "get v4-6" });
        }

        [HttpGet, VersionedRoute("get", 7, 10)]
        public IHttpActionResult Get2()
        {
            return Ok(new { HelloFrom = "get v7-10" });
        }
    }
}