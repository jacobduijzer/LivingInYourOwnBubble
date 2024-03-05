using CattleInformationSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Infrastructure;

public class CowRepository(DatabaseContext databaseContext) : ICowRepository
{
   public async Task AddRange(List<Cow> cows)
   {
      await databaseContext.Cows.AddRangeAsync(cows);
      await databaseContext.SaveChangesAsync();
   }

   public async Task<List<Cow?>> All() =>
      await databaseContext.Cows.ToListAsync();

   public async Task<Cow?> ById(int id) =>
      await databaseContext.Cows
         .Include(cow => cow.FarmCows.OrderBy(fc => fc.StartDate))
         .Include(cow => cow.CowEvents.OrderBy(ce => ce.Order))
         .Include(cow => cow.Farms)
         .FirstOrDefaultAsync(cow => cow != null && cow.Id == id);

   public async Task<Cow?> ByLifeNumber(string lifeNumber) =>
      await databaseContext.Cows
         .Include(cow => cow.FarmCows.OrderBy(fc => fc.StartDate))
         .Include(cow => cow.CowEvents.OrderBy(ce => ce.Order))
         .ThenInclude(cow => cow.Farm)
         .FirstOrDefaultAsync(cow => cow != null && cow.LifeNumber.Equals(lifeNumber));
}