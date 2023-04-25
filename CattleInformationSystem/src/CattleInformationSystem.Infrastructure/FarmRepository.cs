using CattleInformationSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Infrastructure;

public class FarmRepository : IFarmRepository
{
    private readonly DatabaseContext _databaseContext;

    public FarmRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task AddRange(List<Farm> farms)
    {
        await _databaseContext.Farms.AddRangeAsync(farms);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<List<Farm>> All() =>
        await _databaseContext.Farms
            .Include(farm => farm.FarmCows)
            .ToListAsync();

    public async Task<List<Farm>> ByType(FarmType farmType) =>
        await _databaseContext.Farms
            .Where(farm => farm.FarmType == farmType)
            .Include(farm => farm.FarmCows)
            .ToListAsync();

    public async Task<Farm> ById(int farmId) =>
        await _databaseContext.Farms
            .Include(farm => farm.FarmCows.Where(fc => fc.EndDate.HasValue))
            .ThenInclude(farm => farm.Cow)
            .FirstOrDefaultAsync(farm => farm.Id == farmId);

    public async Task SaveChanges()
    {
        await _databaseContext.SaveChangesAsync();
    }
}