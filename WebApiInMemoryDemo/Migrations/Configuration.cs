namespace WebApiInMemoryDemo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApiInMemoryDemo.DAL;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApiInMemoryDemo.DAL.DefaultContext>
    {
        private readonly DefaultContext _ctx;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            if ("true".Equals(Environment.GetEnvironmentVariable("RUN_SEED")))
            {
                _ctx = new DefaultContext();
                _ctx.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE [Urls]");
                Seed(_ctx);
            }
        }

        protected override void Seed(DefaultContext ctx)
        {
            ctx.Urls.AddOrUpdate(x => x.UrlId,
                new Models.Url() { UrlId = 1, Address = "http://google.com", Description = "El primer url" },
                new Models.Url() { UrlId = 2, Address = "http://twitter.com", Description = "El segundo url" },
                new Models.Url() { UrlId = 3, Address = "http://facebook.com", Description = "El tercer url" },
                new Models.Url() { UrlId = 4, Address = "http://reddit.com", Description = "El cuarto url" },
                new Models.Url() { UrlId = 5, Address = "http://youtube.com", Description = "El quinto url" }
            );
        }
    }
}
