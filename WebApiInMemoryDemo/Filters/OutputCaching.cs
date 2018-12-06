using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Filters;

namespace WebApiInMemoryDemo.Filters
{
    public class WebApiOutputCacheAttribute : ActionFilterAttribute
    {
        private readonly int _timeSpan;

        public WebApiOutputCacheAttribute( int timeSpan)
        {
            _timeSpan = timeSpan;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var cacheControl = new CacheControlHeaderValue();
            cacheControl.MaxAge = TimeSpan.FromSeconds(_timeSpan);
            cacheControl.MustRevalidate = true;
            actionExecutedContext.ActionContext.Response.Headers.CacheControl = cacheControl;
        }
    }
}