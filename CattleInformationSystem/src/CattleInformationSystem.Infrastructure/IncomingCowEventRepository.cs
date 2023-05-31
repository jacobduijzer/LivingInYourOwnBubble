using CattleInformationSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Infrastructure;

public class IncomingCowEventRepository : IIncomingCowEventRepository
{
    private readonly DatabaseContext _databaseContext;

    public IncomingCowEventRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task Add(IncomingCowEvent incomingCowEvent)
    {
        await _databaseContext.IncomingCowEvents.AddAsync(incomingCowEvent);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<List<IncomingCowEvent>> ForLifeNumber(string lifeNumber) =>
        await _databaseContext
            .IncomingCowEvents
            .Where(events => events.LifeNumber.Equals(lifeNumber))
            .ToListAsync();
}