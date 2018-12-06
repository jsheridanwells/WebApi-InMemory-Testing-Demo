using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiInMemoryDemo.DAL;

namespace WebApiInMemoryDemo.Models
{
    public interface IUrlRepository
    {
        Task<Url> Add(Url url);
        Task<Url> Get(int id);
        IEnumerable<Url> GetAll();
        Task<Url> Remove(int id);
    }
    public class UrlRepository : IUrlRepository
    {
        private readonly DefaultContext _db;

        public UrlRepository()
        {
            _db = new DefaultContext();    
        }

        public async Task<Url> Add(Url url)
        {
            _db.Urls.Add(url);
            await _db.SaveChangesAsync();
            return url;
        }

        public async Task<Url> Get(int id)
        {
            var url = _db.Urls.FirstOrDefault(u => u.UrlId == id);
            return url;
            
        }

        public IEnumerable<Url> GetAll()
        {
            return _db.Urls;
        }

        public async Task<Url> Remove(int id)
        {
            Url toRemove = _db.Urls.FirstOrDefault<Url>(u => u.UrlId == id);
            _db.Urls.Remove(toRemove);
            await _db.SaveChangesAsync();
            return toRemove;
        }
    }
}