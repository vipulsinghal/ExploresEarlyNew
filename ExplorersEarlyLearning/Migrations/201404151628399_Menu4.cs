namespace ExplorersEarlyLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Menu4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Menu", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Menu", "Name", c => c.String());
        }
    }
}
