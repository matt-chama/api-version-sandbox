using System.Web.Http;

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
    /*
    [RoutePrefix("unversioned")]
    public class UnversionedController : ApiController
    {
        [HttpGet, Route("")]
        public IHttpActionResult Get()
        {
            return Ok(new[] { "value1", "value2" });
        }
    }
    */
}
