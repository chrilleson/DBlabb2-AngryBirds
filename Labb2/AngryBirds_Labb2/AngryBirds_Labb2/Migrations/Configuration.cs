namespace AngryBirds_Labb2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AngryBirds_Labb2.ScoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AngryBirds_Labb2.ScoreContext";
        }

        protected override void Seed(AngryBirds_Labb2.ScoreContext context)
        {
            //  This method will be called after migrating to the latest version.
            var level1 = context.Levels.Add(new Level { NumberOfBirds = 10, NameOfLevel = "KnutsLevel" });
            var level2 = context.Levels.Add(new Level { NumberOfBirds = 8, NameOfLevel = "Chrillelevel" });
            var player1 = context.Players.Add(new Player { PlayerName = "Knut" });
            var player2 = context.Players.Add(new Player { PlayerName = "Christoffer" });

            context.SaveChanges();

            context.Scores.Add(new Score { Player = player1, Level = level1, Highscore = 1 });
            context.Scores.Add(new Score { Player = player2, Level = level1, Highscore = 4 });
            context.Scores.Add(new Score { Player = player2, Level = level2, Highscore = 7 });
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
