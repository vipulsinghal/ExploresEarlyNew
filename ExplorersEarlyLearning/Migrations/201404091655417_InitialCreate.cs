namespace ExplorersEarlyLearning.Migrations
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
                    })
                .PrimaryKey(t => t.MenuId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Menu");
        }
    }
}
