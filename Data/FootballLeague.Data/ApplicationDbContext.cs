using FootballLeague.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Fixture> Fixtures { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Match> Matches { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
     
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Match>()
                  .HasOne(m => m.HomeTeam)
                  .WithMany(t => t.HomeMatches)
                  .HasForeignKey(m => m.HomeTeamId);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.Fixture)
                .WithOne(f => f.Team)
                .HasForeignKey<Fixture>(f => f.TeamId);

        }
    }
}
