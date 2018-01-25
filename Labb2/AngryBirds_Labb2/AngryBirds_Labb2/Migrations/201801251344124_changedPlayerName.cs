namespace AngryBirds_Labb2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedPlayerName : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Players", "PlayerName", "Username");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Players", "PlayerName", "Username");
        }
    }
}
