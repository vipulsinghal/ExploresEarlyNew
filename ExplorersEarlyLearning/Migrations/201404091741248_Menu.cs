namespace ExplorersEarlyLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Menu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menu", "CreateDate", c => c.DateTime());
            AddColumn("dbo.Menu", "UpdateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menu", "UpdateDate");
            DropColumn("dbo.Menu", "CreateDate");
        }
    }
}
