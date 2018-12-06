using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApiInMemoryDemo.Handlers
{
    public class WebApiKeyHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage req, CancellationToken tok)
        {
            string apiKey = HttpUtility.ParseQueryString(req.RequestUri.Query)
                .Get("apikey");

            if (string.IsNullOrEmpty(apiKey))
            {
                return SendError("Falta la llave del API", HttpStatusCode.Forbidden);
            }
            else
            {
                return base.SendAsync(req, tok);
            }


        }

        private Task<HttpResponseMessage> SendError(string error, HttpStatusCode code)
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent(error);
            response.StatusCode = code;
            return Task<HttpResponseMessage>.Factory.StartNew(() => response);
        }
    }
}