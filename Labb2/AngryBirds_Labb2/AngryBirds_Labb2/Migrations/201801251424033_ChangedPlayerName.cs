namespace AngryBirds_Labb2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPlayerName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "Username", c => c.String(nullable: false));
            DropColumn("dbo.Players", "PlayerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "PlayerName", c => c.String(nullable: false));
            DropColumn("dbo.Players", "Username");
        }
    }
}
