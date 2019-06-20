using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace WebApi.Tests
{
    public class ResponseModel
    {
        public string HelloFrom { get; set; }
    }

    [TestClass]
    public class VersioningTest
    {
        [DataRow(1, HttpStatusCode.NotFound, null)]
        [DataRow(4, HttpStatusCode.OK, "get v4-6")]
        [DataRow(8, HttpStatusCode.OK, "get v7-10")]
        [DataRow(11, HttpStatusCode.NotFound, null)]
        [DataTestMethod]
        public async Task Test(
            int version,
            HttpStatusCode expectedStatus,
            string helloFrom = null)
        {
            using (var server = TestServer.Create<OwinTestConfiguration>())
            {
                using (var client = new HttpClient(server.Handler))
                {
                    var response = await client.GetAsync($"http://testserver/api/v{version}/versioned/get");

                    Assert.AreEqual(expectedStatus, response.StatusCode);

                    if (expectedStatus == HttpStatusCode.OK)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var model = JsonConvert.DeserializeObject<ResponseModel>(jsonResponse);

                        Assert.AreEqual(helloFrom, model.HelloFrom);
                    }
                }
            }
        }
    }
}
