using CIS.Domain;
using Microsoft.EntityFrameworkCore;

namespace CIS.Infrastructure;

public class CattleInformationDatabaseContext : DbContext
{
    public CattleInformationDatabaseContext(DbContextOptions<CattleInformationDatabaseContext> options) : base(options)
    {

    }

    public DbSet<Cow> Cows => Set<Cow>();
    public DbSet<RawCowData?> RawCowData => Set<RawCowData>();
}