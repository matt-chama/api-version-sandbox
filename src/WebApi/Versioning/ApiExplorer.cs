using System.Collections.ObjectModel;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApi.Versioning
{
    public class ApiExplorer : IApiExplorer
    {
        private readonly HttpConfiguration _config;

        public Collection<ApiDescription> ApiDescriptions => GetApiDescriptions();

        public ApiExplorer(HttpConfiguration config)
        {
            _config = config;
        }

        private Collection<ApiDescription> GetApiDescriptions()
        {
            return new Collection<ApiDescription>();
        }
    }
}
