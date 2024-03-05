using CattleInformationSystem.Domain;
using CattleInformationSystem.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CattleInformationSystem.Infrastructure;

public class FarmRepository(DatabaseContext databaseContext) : IFarmRepository
{
    public async Task AddRange(List<Farm> farms)
    {
        await databaseContext.Farms.AddRangeAsync(farms);
        await databaseContext.SaveChangesAsync();
    }

    public async Task AddOrUpdate(Farm farm)
    {
        var existingFarm = await databaseContext.Farms.FirstOrDefaultAsync(f => f.UBN.Equals(farm.UBN));
        if (existingFarm != null)
            existingFarm.FarmType = farm.FarmType;
        else
            await databaseContext.Farms.AddAsync(farm);
        
        await databaseContext.SaveChangesAsync();
    }

    public async Task<List<Farm>> All() =>
        await databaseContext.Farms
            .Include(farm => farm.FarmCows.Where(fc => !fc.EndDate.HasValue))
            .ThenInclude(x => x.Cow)
            .ToListAsync();

    public async Task<List<Farm>> ByType(FarmType farmType) =>
        await databaseContext.Farms
            .Where(farm => farm.FarmType == farmType)
            .Include(farm => farm.FarmCows)
            .ToListAsync();

    public async Task<Farm> ById(int farmId) =>
        await databaseContext.Farms
            .Include(farm => farm.FarmCows.Where(fc => !fc.EndDate.HasValue))
            .ThenInclude(x => x.Cow)
            .FirstOrDefaultAsync(farm => farm.Id == farmId);

    public async Task<Farm> ByIdWithHistory(int farmId) =>
        await databaseContext.Farms
            .Include(farm => farm.FarmCows)
            .ThenInclude(x => x.Cow)
            .FirstOrDefaultAsync(farm => farm.Id == farmId);

    public async Task SaveChanges()
    {
        await databaseContext.SaveChangesAsync();
    }
}