using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiInMemoryDemo.Filters;
using WebApiInMemoryDemo.Models;

namespace WebApiInMemoryDemo.Controllers
{
    public class UrlController : ApiController
    {
        private readonly IUrlRepository _repo = new UrlRepository();

        [WebApiOutputCache(60)]
        public IHttpActionResult Get()
        {
            var urls = _repo.GetAll();
            return Ok(urls);
        }

        public IHttpActionResult Get(int id)
        {
            var url = _repo.Get(id);
            return Ok(url.Result);
        }

        [HttpPost]        
        public IHttpActionResult Add([FromBody] Url url)
        {
            _repo.Add(url);
            return Created(Request.RequestUri, url);
        }

        public IHttpActionResult Delete(int id)
        {
            _repo.Remove(id);
            return Ok();
        }
    }
}
