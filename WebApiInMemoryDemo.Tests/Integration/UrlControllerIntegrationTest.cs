using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiInMemoryDemo.Handlers;
using Newtonsoft.Json;
using WebApiInMemoryDemo.Tests.Helpers;
using System.Net.Http.Formatting;
using System.Net;

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
        public void GetAllUrls()
        {
            var client = new HttpClient(_server);
            var request = _helpers.CreateRequest(_url, "api/url?apikey=test", "application/json", HttpMethod.Get);
            string expectedResponse = _fixtures.Urls_GetAll_ExpectedResponse();

            using (HttpResponseMessage response = client.SendAsync(request).Result)
            {
                Assert.IsNotNull(response.Content);
                Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(expectedResponse, response.Content.ReadAsStringAsync().Result);
            }

            client.Dispose();
            request.Dispose();
        }        

        [TestMethod]
        public void GetUrlById()
        {
            int id = new Random().Next(1, 6);
            var client = new HttpClient(_server);
            var request = _helpers.CreateRequest(_url, $"api/url/{id}?apikey=test", "application/json", HttpMethod.Get);
            string expectedResponse = _fixtures.Urls_GetById_ExpectedResponse(id - 1);

            using (HttpResponseMessage response = client.SendAsync(request).Result)
            {
                Assert.IsNotNull(response.Content);
                Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(expectedResponse, response.Content.ReadAsStringAsync().Result);
            }

            client.Dispose();
            request.Dispose();
        }

        [TestMethod]
        public void AddUrl()
        {
            var client = new HttpClient(_server);
            var postBody = _fixtures.Urls_Post_Request();
            var request = _helpers.CreateRequest(_url, "api/url?apikey=test", "application/json", HttpMethod.Post, postBody, new JsonMediaTypeFormatter());            

            using (HttpResponseMessage response = client.SendAsync(request).Result)
            {
                Assert.IsNotNull(response.Content);
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
            }

            client.Dispose();
            request.Dispose();
        }

        [TestMethod]
        public void DeleteUrl()
        {
            var client = new HttpClient(_server);
            var request = _helpers.CreateRequest(_url, "api/url/6?apikey=test", "application/json", HttpMethod.Delete);
            using (HttpResponseMessage response = client.SendAsync(request).Result)
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            client.Dispose();
            request.Dispose();
        }

        public void Dispose()
        {
          _server.Dispose();
        }
    }
}
