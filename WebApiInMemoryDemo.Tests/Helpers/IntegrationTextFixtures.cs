using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiInMemoryDemo.Models;

namespace WebApiInMemoryDemo.Tests.Helpers
{
    class IntegrationTextFixtures
    {
        public string Urls_GetAll_ExpectedResponse()
        {
            var urls = new List<Url>
            {
                new Models.Url() { UrlId = 1, Address = "http://google.com", Description = "El primer url" },
                new Models.Url() { UrlId = 2, Address = "http://twitter.com", Description = "El segundo url" },
                new Models.Url() { UrlId = 3, Address = "http://facebook.com", Description = "El tercer url" },
                new Models.Url() { UrlId = 4, Address = "http://reddit.com", Description = "El cuarto url" },
                new Models.Url() { UrlId = 5, Address = "http://youtube.com", Description = "El quinto url" }
            };
            return JsonConvert.SerializeObject(urls);
        }
    }
}
