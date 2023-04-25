using CattleInformationSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FarmCow>().HasKey(sc => new { sc.FarmId, sc.CowId });
        
        modelBuilder.Entity<Farm>()
            .HasMany(farm => farm.Cows)
            .WithMany(cow => cow.Farms)
            .UsingEntity<FarmCow>(
                l => l.HasOne<Cow>(e => e.Cow).WithMany(e => e.FarmCows).HasForeignKey(e => e.CowId),
                r => r.HasOne<Farm>(e => e.Farm).WithMany(e => e.FarmCows).HasForeignKey(e => e.FarmId));
    }

    public DbSet<Farm> Farms => Set<Farm>();
    public DbSet<Cow?> Cows => Set<Cow>();
    public DbSet<FarmCow> FarmCows => Set<FarmCow>();
}