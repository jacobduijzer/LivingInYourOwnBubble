using CattleInformationSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Infrastructure;

public class FarmCowRepository(DatabaseContext databaseContext) : IFarmCowRepository
{
    public async Task Add(FarmCow farmCow)
    {
        await databaseContext.FarmCows.AddAsync(farmCow);
        await databaseContext.SaveChangesAsync();
    }

    public async Task Update(FarmCow farmCow)
    {
        databaseContext.FarmCows.Attach(farmCow);
        databaseContext.Entry(farmCow).State = EntityState.Modified;
        await databaseContext.SaveChangesAsync();
    }
}