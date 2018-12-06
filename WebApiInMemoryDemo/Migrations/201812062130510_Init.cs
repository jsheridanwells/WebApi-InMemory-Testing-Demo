namespace WebApiInMemoryDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Urls",
                c => new
                    {
                        UrlId = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.UrlId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Urls");
        }
    }
}
