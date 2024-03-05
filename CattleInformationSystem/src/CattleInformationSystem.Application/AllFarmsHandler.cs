using CattleInformationSystem.Domain;

namespace CattleInformationSystem.Application;

public class AllFarmsHandler(IFarmRepository farms)
{
    public async Task<List<Farm>> Handler() =>
        await farms.All();
}