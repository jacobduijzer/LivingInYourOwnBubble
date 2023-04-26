using CattleInformationSystem.Domain;

namespace CattleInformationSystem.TestData;

public class DataTester
{
    private readonly IFarmRepository _farms;

    public DataTester(IFarmRepository farms)
    {
        _farms = farms;
    }

    public async Task Test()
    {
        var farmData = await _farms.All();
    }
}