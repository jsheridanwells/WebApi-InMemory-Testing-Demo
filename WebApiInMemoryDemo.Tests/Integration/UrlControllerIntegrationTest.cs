using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiInMemoryDemo.Handlers;
using Newtonsoft.Json;
using WebApiInMemoryDemo.Tests.Helpers;

namespace WebApiInMemoryDemo.Tests.Integration
{
    [TestClass]
    public class UrlControllerIntegrationTest
    {
        private HttpServer _server;
        private readonly IntegrationTestHelper _helpers;
        private readonly IntegrationTextFixtures _fixtures;
        private readonly string _url = "http://localhost:56564/";

        public UrlControllerIntegrationTest()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.MessageHandlers.Add(new WebApiKeyHandler());
            _server = new HttpServer(config);
            _helpers = new IntegrationTestHelper();
            _fixtures = new IntegrationTextFixtures();
        }

        [TestMethod]
        public void Get()
        {
            var client = new HttpClient(_server);
            var request = _helpers.CreateRequest(_url, "api/url?apikey=test", "application/json", HttpMethod.Get);
            string expectedResponse = _fixtures.Urls_GetAll_ExpectedResponse();


            using (HttpResponseMessage response = client.SendAsync(request).Result)
            {
                Assert.IsNotNull(response.Content);
                Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
                Assert.AreEqual(expectedResponse, response.Content.ReadAsStringAsync().Result);

            }
            
        }

        

        public void Dispose()
        {
          _server.Dispose();
        }
    }
}
