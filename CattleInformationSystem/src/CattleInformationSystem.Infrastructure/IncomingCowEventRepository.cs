using CattleInformationSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Infrastructure;

public class IncomingCowEventRepository(DatabaseContext databaseContext) : IIncomingCowEventRepository
{
    public async Task Add(IncomingCowEvent incomingCowEvent)
    {
        await databaseContext.IncomingCowEvents.AddAsync(incomingCowEvent);
        await databaseContext.SaveChangesAsync();
    }

    public async Task<List<IncomingCowEvent>> ForLifeNumber(string lifeNumber) =>
        await databaseContext
            .IncomingCowEvents
            .Where(events => events.LifeNumber.Equals(lifeNumber))
            .ToListAsync();
}