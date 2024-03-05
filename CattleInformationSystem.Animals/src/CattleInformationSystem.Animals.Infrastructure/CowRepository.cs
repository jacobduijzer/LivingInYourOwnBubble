using CattleInformationSystem.Animals.Infrastructure.AnimalCowMapping;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Animals.Infrastructure;

public class CowRepository(DatabaseContext databaseContext) : ICowRepository
{
    public async Task<Cow> ByLifeNumber(string lifeNumber) =>
        await databaseContext.Cows
            .Include(cow => cow.FarmCows)
            .Include(cow => cow.CowEvents)
            .FirstAsync(c => c.LifeNumber.Equals(lifeNumber));

    public async Task Save(Cow cow)
    {
        await databaseContext.Cows.AddAsync(cow);
        await databaseContext.SaveChangesAsync();
    }

    public async Task Update(Cow cow)
    {
        databaseContext.Cows.Update(cow);
        await databaseContext.SaveChangesAsync();
    }
}