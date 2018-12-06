using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiInMemoryDemo.Models
{
    public interface IUrlRepository
    {
        IQueryable<Url> GetAll();
        Url Get(int id);
        Url Add(Url url);
        Url Remove(int id);
    }
    public class UrlRepository : IUrlRepository
    {
        private List<Url> urls = new List<Url>();
        private int nextId = 1;
        public UrlRepository()
        {
            this.Add(new Url()
            {
                UrlId = 1,
                Address = "https://google.com",
                Description = "Eso es el primer url"
            });

            this.Add(new Url()
            {
                UrlId = 2,
                Address = "https://twitter.com",
                Description = "Eso es el segundo url"
            });

            this.Add(new Url()
            {
                UrlId = 3,
                Address = "https://apple.com",
                Description = "Eso es el tercer url"
            });
        }

        public Url Add(Url url)
        {
            url.UrlId = nextId++;
            this.urls.Add(url);
            return url;
        }

        public Url Get(int id)
        {
            return this.urls.Find(u => u.UrlId == id);
        }

        public IQueryable<Url> GetAll()
        {
            return this.urls.AsQueryable();
        }

        public Url Remove(int id)
        {
            Url removeMe = this.urls.Find(u => u.UrlId == id);
            this.urls.Remove(removeMe);
            return removeMe;
        }
    }
}