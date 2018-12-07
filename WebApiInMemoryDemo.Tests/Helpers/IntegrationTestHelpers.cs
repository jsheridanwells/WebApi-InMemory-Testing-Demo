using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApiInMemoryDemo.Tests.Helpers
{
    public class IntegrationTestHelper
    {
        public HttpRequestMessage CreateRequest(string domain, string url, string mthv, HttpMethod method)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(domain + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            request.Method = method;
            return request;
        }

        public HttpRequestMessage CreateRequest<T>(string domain, string url, string mthv, HttpMethod method, T postBody, MediaTypeFormatter formatter
            )
        {
            HttpRequestMessage request = CreateRequest(domain, url, mthv, method);
            request.Content = new ObjectContent<T>(postBody, formatter);
            return request;
        }
    }
}
