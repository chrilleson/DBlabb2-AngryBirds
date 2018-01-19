using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AngryBirds_Labb2
{
    public class ScoreContext : DbContext
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AngryBirdsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ScoreContext() : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasMany(s => s.Scoreboard)
                .WithRequired(p => p.Player);
            modelBuilder.Entity<Level>()
                .HasMany(s => s.Scoreboard)
                .WithRequired(l => l.Level);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Score> Scores { get; set; }

    }
}
