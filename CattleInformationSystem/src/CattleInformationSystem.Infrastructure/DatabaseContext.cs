using CattleInformationSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Infrastructure;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Farm> Farms => Set<Farm>();
    public DbSet<Cow?> Cows => Set<Cow>();
    public DbSet<FarmCow> FarmCows => Set<FarmCow>();
    public DbSet<CowEvent> CowEvents => Set<CowEvent>();
    public DbSet<IncomingCowEvent> IncomingCowEvents => Set<IncomingCowEvent>();
    public DbSet<AnimalCategory> AnimalCategories => Set<AnimalCategory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Farm>()
            .HasMany(f => f.Cows)
            .WithMany(c => c.Farms)
            .UsingEntity<FarmCow>();
    }
}