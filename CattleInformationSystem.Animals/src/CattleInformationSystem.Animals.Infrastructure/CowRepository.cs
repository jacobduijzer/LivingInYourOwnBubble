using CattleInformationSystem.Animals.Infrastructure.AnimalCowMapping;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Animals.Infrastructure;

public class CowRepository : ICowRepository
{
    private readonly DatabaseContext _databaseContext;

    public CowRepository(DatabaseContext databaseContext) => 
        _databaseContext = databaseContext;
    
    public async Task<Cow> ByLifeNumber(string lifeNumber) =>
        await _databaseContext.Cows
            .Include(cow => cow.FarmCows)
            .Include(cow => cow.CowEvents)
            .FirstAsync(c => c.LifeNumber.Equals(lifeNumber));

    public async Task Save(Cow cow)
    {
        await _databaseContext.Cows.AddAsync(cow);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task Update(Cow cow)
    {
        _databaseContext.Cows.Update(cow);
        await _databaseContext.SaveChangesAsync();
    }
}