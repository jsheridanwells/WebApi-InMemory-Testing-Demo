using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApiInMemoryDemo.Models;

namespace WebApiInMemoryDemo.DAL
{
    public class DefaultContext : DbContext
    {
        public DefaultContext() : base("DefaultConnection") { }

        public DbSet<Url> Urls { get; set; }
    }
}