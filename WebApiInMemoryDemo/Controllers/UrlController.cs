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
        public IQueryable<Url> Get()
        {
            return _repo.GetAll();
        }

        public Url Get(int id)
        {
            return _repo.Get(id);
        }

        [HttpPost]
        public Url Add([FromBody] Url url)
        {
            return _repo.Add(url);
        }

        public Url Delete(int id)
        {
            return _repo.Remove(id);
        }
    }
}
