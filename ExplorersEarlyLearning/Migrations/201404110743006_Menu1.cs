namespace ExplorersEarlyLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Menu1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menu", "NavCurrentIndex", c => c.Int(nullable: false));
            AddColumn("dbo.Menu", "NavSubCurrentIndex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menu", "NavSubCurrentIndex");
            DropColumn("dbo.Menu", "NavCurrentIndex");
        }
    }
}
