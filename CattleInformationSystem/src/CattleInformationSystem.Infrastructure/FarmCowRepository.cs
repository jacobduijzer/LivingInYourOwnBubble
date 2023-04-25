using CattleInformationSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Infrastructure;

public class FarmCowRepository : IFarmCowRepository
{
    private readonly DatabaseContext _databaseContext;

    public FarmCowRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task Add(FarmCow farmCow)
    {
        await _databaseContext.FarmCows.AddAsync(farmCow);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task Update(FarmCow farmCow)
    {
        _databaseContext.FarmCows.Attach(farmCow);
        _databaseContext.Entry(farmCow).State = EntityState.Modified;
        await _databaseContext.SaveChangesAsync();
    }
}