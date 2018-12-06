using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiInMemoryDemo.Handlers;
using Newtonsoft.Json;

namespace WebApiInMemoryDemo.Tests.Integration
{
    [TestClass]
    public class UrlControllerIntegrationTest
    {
        private HttpServer _server;
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
        }

        [TestMethod]
        public void Get()
        {
            var client = new HttpClient(_server);
            var request = createRequest("api/url?apikey=test", "application/json", HttpMethod.Get);
            string expectedResponse = "[{\"UrlId\":1,\"Address\":\"https://google.com\",\"Title\":null,\"Description\":\"Eso es el primer url\"},{\"UrlId\":2,\"Address\":\"https://twitter.com\",\"Title\":null,\"Description\":\"Eso es el segundo url\"},{\"UrlId\":3,\"Address\":\"https://apple.com\",\"Title\":null,\"Description\":\"Eso es el tercer url\"}]";


            using (HttpResponseMessage response = client.SendAsync(request).Result)
            {
                Assert.IsNotNull(response.Content);
                Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
                Assert.AreEqual(expectedResponse, response.Content.ReadAsStringAsync().Result);

            }
            
        }

        private HttpRequestMessage createRequest(string url, string mthv, HttpMethod method)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(_url + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            request.Method = method;
            return request;
        }

        public void Dispose()
        {
          _server.Dispose();
        }
    }
}
