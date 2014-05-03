namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class webpage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WebPage", "Parent_Id", "dbo.WebPage");
            DropIndex("dbo.WebPage", new[] { "Parent_Id" });
            AddColumn("dbo.WebPage", "ParentId", c => c.Int());
            AddForeignKey("dbo.WebPage", "ParentId", "dbo.WebPage", "Id");
            CreateIndex("dbo.WebPage", "ParentId");
            DropColumn("dbo.WebPage", "Parent_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WebPage", "Parent_Id", c => c.Int());
            DropIndex("dbo.WebPage", new[] { "ParentId" });
            DropForeignKey("dbo.WebPage", "ParentId", "dbo.WebPage");
            DropColumn("dbo.WebPage", "ParentId");
            CreateIndex("dbo.WebPage", "Parent_Id");
            AddForeignKey("dbo.WebPage", "Parent_Id", "dbo.WebPage", "Id");
        }
    }
}
