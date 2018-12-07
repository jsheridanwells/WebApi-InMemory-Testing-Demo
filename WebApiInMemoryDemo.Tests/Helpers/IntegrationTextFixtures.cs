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
        List<Url> urls = new List<Url>
        {
            new Models.Url() { UrlId = 1, Address = "http://google.com", Description = "El primer url" },
            new Models.Url() { UrlId = 2, Address = "http://twitter.com", Description = "El segundo url" },
            new Models.Url() { UrlId = 3, Address = "http://facebook.com", Description = "El tercer url" },
            new Models.Url() { UrlId = 4, Address = "http://reddit.com", Description = "El cuarto url" },
            new Models.Url() { UrlId = 5, Address = "http://youtube.com", Description = "El quinto url" }
        };

        public string Urls_GetAll_ExpectedResponse()
        {
            return JsonConvert.SerializeObject(urls);
        }

        public string Urls_GetById_ExpectedResponse(int id)
        {
            return JsonConvert.SerializeObject(urls[id]);            
        }

        public object Urls_Post_Request()
        {
            Url newUrl = new Url { Address = "http://pitchfork.com", Description = "my new url" };
            return JsonConvert.SerializeObject(newUrl);
        }
    }
}
