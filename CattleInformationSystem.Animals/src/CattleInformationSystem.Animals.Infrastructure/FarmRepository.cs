using CattleInformationSystem.Animals.Domain;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Animals.Infrastructure;

public class FarmRepository : IFarmRepository
{
    private readonly DatabaseContext _databaseContext;

    public FarmRepository(DatabaseContext databaseContext) => 
        _databaseContext = databaseContext;
    
    public async Task<IReadOnlyCollection<Farm>> ByUbns(string?[] ubns) =>
        await _databaseContext.Farms
            .Where(farm => ubns.Any(ubn => farm.UBN.Equals(ubn)))
            .ToListAsync();

    public async Task<IReadOnlyCollection<Farm>> ByFarmIds(int[] farmIds) =>
       await _databaseContext.Farms
           .Where(farm => farmIds.Any(id => farm.Id.Equals(id)))
           .ToListAsync();
}