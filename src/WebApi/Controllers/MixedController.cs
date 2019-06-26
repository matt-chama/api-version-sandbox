using System.Web.Http;
using WebApi.Versioning;

namespace WebApi.Controllers
{
    [RoutePrefix("mixed")]
    public class MixedController : ApiController
    {
        [HttpGet, Route("get")]
        public IHttpActionResult Get()
        {
            return Ok(new { HelloFrom = "mixed unversioned" });
        }

        [HttpGet, VersionedRoute("get", 4, 6)]
        public IHttpActionResult Get2()
        {
            return Ok(new { HelloFrom = "mixed versioned" });
        }
    }
}
