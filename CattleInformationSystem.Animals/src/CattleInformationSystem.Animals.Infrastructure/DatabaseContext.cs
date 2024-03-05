using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.Animals.Infrastructure.AnimalCowMapping;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Animals.Infrastructure;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Cow?> Cows => Set<Cow?>();
    public DbSet<AnimalCategory> AnimalCategories => Set<AnimalCategory>();
    public DbSet<Farm> Farms => Set<Farm>();
}