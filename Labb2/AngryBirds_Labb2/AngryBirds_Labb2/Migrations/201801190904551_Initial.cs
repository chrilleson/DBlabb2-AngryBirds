namespace AngryBirds_Labb2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        LevelID = c.Int(nullable: false, identity: true),
                        NameOfLevel = c.String(nullable: false),
                        NumberOfBirds = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LevelID);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        ScoreID = c.Int(nullable: false, identity: true),
                        Highscore = c.Int(nullable: false),
                        Player_PlayerID = c.Int(nullable: false),
                        Level_LevelID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ScoreID)
                .ForeignKey("dbo.Players", t => t.Player_PlayerID, cascadeDelete: true)
                .ForeignKey("dbo.Levels", t => t.Level_LevelID, cascadeDelete: true)
                .Index(t => t.Player_PlayerID)
                .Index(t => t.Level_LevelID);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerID = c.Int(nullable: false, identity: true),
                        PlayerName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Scores", "Level_LevelID", "dbo.Levels");
            DropForeignKey("dbo.Scores", "Player_PlayerID", "dbo.Players");
            DropIndex("dbo.Scores", new[] { "Level_LevelID" });
            DropIndex("dbo.Scores", new[] { "Player_PlayerID" });
            DropTable("dbo.Players");
            DropTable("dbo.Scores");
            DropTable("dbo.Levels");
        }
    }
}
