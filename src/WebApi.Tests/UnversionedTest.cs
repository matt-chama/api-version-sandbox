using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace WebApi.Tests
{
    [TestClass]
    public class UnversionedTest
    {
        [TestMethod]
        public async Task WhenVersionNeutralEndpointIsCalled_ThenResultIsExpected()
        {
            using (var server = TestServer.Create<OwinTestConfiguration>())
            {
                using (var client = new HttpClient(server.Handler))
                {
                    var response = await client.GetAsync("http://testserver/unversioned/get");

                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<ResponseModel>(jsonResponse);

                    Assert.AreEqual("unversioned", model.HelloFrom);
                }
            }
        }
    }
}