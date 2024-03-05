using CattleInformationSystem.Animals.Domain;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Animals.Infrastructure;

public class FarmRepository(DatabaseContext databaseContext) : IFarmRepository
{
    public async Task<IReadOnlyCollection<Farm>> ByUbns(string?[] ubns) =>
        await databaseContext.Farms
            .Where(farm => ubns.Any(ubn => farm.UBN.Equals(ubn)))
            .ToListAsync();

    public async Task<IReadOnlyCollection<Farm>> ByFarmIds(int[] farmIds) =>
       await databaseContext.Farms
           .Where(farm => farmIds.Any(id => farm.Id.Equals(id)))
           .ToListAsync();
}