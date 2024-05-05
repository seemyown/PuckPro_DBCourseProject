using Microsoft.EntityFrameworkCore;

namespace PuckPro.Models;

public class PuckDBContext : DbContext
{
    public PuckDBContext(DbContextOptions<PuckDBContext> options)
        : base(options)
    {
        
    }
    
    public DbSet<Countries> Countries { get; set; }
    public DbSet<Cities> Cities { get; set; }
    public DbSet<Players> Players { get; set; }
    public DbSet<PlayersProperties> PlayersProperties { get; set; }
    public DbSet<PlayerSalary> PlayerSalaries { get; set; }
    public DbSet<PlayersRating> PlayersRatings { get; set; }
    public DbSet<PlayerStatisticPerGame> PlayerStatisticPerGames { get; set; }
    public DbSet<Teams> Teams { get; set; }
    public DbSet<TeamRoasters> TeamRoasters { get; set; }
    public DbSet<TeamStatistic> TeamStatistics { get; set; }
    public DbSet<Games> Games { get; set; }
    public DbSet<GamesResults> GamesResults { get; set; }
    public DbSet<GameRevenue> GameRevenues { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Games>(entity =>
        {
            entity.HasOne(g => g.HomeTeam)
                .WithMany(t => t.HomeGames)
                .HasForeignKey(g => g.HomeTeamId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(g => g.GuestTeam)
                .WithMany(t => t.AwayGames)
                .HasForeignKey(g => g.GuestTeamId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

}