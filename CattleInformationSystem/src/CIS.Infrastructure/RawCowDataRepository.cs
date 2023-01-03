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

    public async Task ProcessRawCowData() =>
        await _databaseContext.Database.ExecuteSqlAsync($"call ProcessRawCowData()");
}