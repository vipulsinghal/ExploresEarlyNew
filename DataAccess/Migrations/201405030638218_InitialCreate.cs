namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        MenuId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PageTitle = c.String(),
                        ParentMenuId = c.Int(),
                        OrderIndex = c.Short(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        PageType = c.Int(),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MenuId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.WebPage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Contents = c.String(),
                        Type = c.Int(nullable: false),
                        DisplaySequence = c.Int(nullable: false),
                        IsPublished = c.Boolean(nullable: false),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebPage", t => t.Parent_Id)
                .Index(t => t.Parent_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.WebPage", new[] { "Parent_Id" });
            DropForeignKey("dbo.WebPage", "Parent_Id", "dbo.WebPage");
            DropTable("dbo.WebPage");
            DropTable("dbo.User");
            DropTable("dbo.Menu");
        }
    }
}
