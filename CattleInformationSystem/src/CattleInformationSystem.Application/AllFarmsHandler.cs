using CattleInformationSystem.Domain;

namespace CattleInformationSystem.Application;

public class AllFarmsHandler
{
    private readonly IFarmRepository _farms;

    public AllFarmsHandler(IFarmRepository farms)
    {
        _farms = farms;
    }

    public async Task<List<Farm>> Handler() =>
        await _farms.All();
}