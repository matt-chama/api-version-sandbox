using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("unversioned")]
    public class UnversionedController : ApiController
    {
        [HttpGet, Route("")]
        public IHttpActionResult DefaultGet()
        {
            return Ok(new { HelloFrom = "unversioned default" });
        }

        [HttpGet, Route("get")]
        public IHttpActionResult Get()
        {
            return Ok(new { HelloFrom = "unversioned" });
        }
    }
}