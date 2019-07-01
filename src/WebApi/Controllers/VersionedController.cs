using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using WebApi.Versioning;

namespace WebApi.Controllers
{
    [RoutePrefix("versioned")]
    public class VersionedController : ApiController
    {
        [HttpGet, VersionedRoute("get", 4, 6)]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(ModelV1))]
        public IHttpActionResult Get()
        {
            return Ok(new { HelloFrom = "get v4-6" });
        }

        [HttpGet, VersionedRoute("get", 7, 10)]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(ModelV2))]
        public IHttpActionResult Get2()
        {
            return Ok(new { HelloFrom = "get v7-10" });
        }
    }

    public class ModelV1
    {
        public string HelloFrom { get; set; }
    }

    public class ModelV2
    {
        public string HelloFrom { get; set; }
    }
}