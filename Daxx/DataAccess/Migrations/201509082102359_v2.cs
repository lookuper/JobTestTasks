namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Province",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        AgreeToWorkForFood = c.Boolean(nullable: false),
                        Country_Id = c.Int(),
                        Province_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.Country_Id)
                .ForeignKey("dbo.Province", t => t.Province_Id)
                .Index(t => t.Country_Id)
                .Index(t => t.Province_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "Province_Id", "dbo.Province");
            DropForeignKey("dbo.User", "Country_Id", "dbo.Country");
            DropForeignKey("dbo.Province", "CountryId", "dbo.Country");
            DropIndex("dbo.User", new[] { "Province_Id" });
            DropIndex("dbo.User", new[] { "Country_Id" });
            DropIndex("dbo.Province", new[] { "CountryId" });
            DropTable("dbo.User");
            DropTable("dbo.Province");
            DropTable("dbo.Country");
        }
    }
}
