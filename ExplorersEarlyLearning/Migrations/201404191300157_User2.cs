namespace ExplorersEarlyLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.User", "LastName", c => c.String());
            AddColumn("dbo.User", "MobileNumber", c => c.String());
            AddColumn("dbo.User", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "LastLoginDate", c => c.DateTime());
            AddColumn("dbo.User", "CreateDate", c => c.DateTime());
            AddColumn("dbo.User", "UpdateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "UpdateDate");
            DropColumn("dbo.User", "CreateDate");
            DropColumn("dbo.User", "LastLoginDate");
            DropColumn("dbo.User", "IsActive");
            DropColumn("dbo.User", "MobileNumber");
            DropColumn("dbo.User", "LastName");
            DropColumn("dbo.User", "FirstName");
        }
    }
}
