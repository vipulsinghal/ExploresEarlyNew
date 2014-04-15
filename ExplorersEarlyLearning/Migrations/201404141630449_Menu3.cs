namespace ExplorersEarlyLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Menu3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Menu", "NavCurrentIndex");
            DropColumn("dbo.Menu", "NavSubCurrentIndex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Menu", "NavSubCurrentIndex", c => c.Int(nullable: false));
            AddColumn("dbo.Menu", "NavCurrentIndex", c => c.Int(nullable: false));
        }
    }
}
