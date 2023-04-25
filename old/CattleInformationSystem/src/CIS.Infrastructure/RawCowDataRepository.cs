using System.Linq.Expressions;
using CIS.Domain;
using Microsoft.EntityFrameworkCore;

namespace CIS.Infrastructure;

public class RawCowDataRepository 
{
    private readonly CattleInformationDatabaseContext _databaseContext;

    public RawCowDataRepository(CattleInformationDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<int> InsertRawCow(RawCowData rawCowData)
    {
        _databaseContext.RawCowData.Add(rawCowData);
        await _databaseContext.SaveChangesAsync();
        return rawCowData.Id;
    }

    public async Task ProcessRawCowData() =>
        await _databaseContext.Database.ExecuteSqlAsync($"call ProcessRawCowData()");
}