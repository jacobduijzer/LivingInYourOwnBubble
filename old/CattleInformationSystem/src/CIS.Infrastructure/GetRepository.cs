using System.Linq.Expressions;
using CIS.Domain;
using Microsoft.EntityFrameworkCore;

namespace CIS.Infrastructure;

public class GetRepository<TEntity> : IGetRepository<TEntity> where TEntity : class
{
    private readonly CattleInformationDatabaseContext _databaseContext;

    public GetRepository(CattleInformationDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> expression) =>
        await _databaseContext
            .Set<TEntity>()
            .Where(expression)
            .ToListAsync();
}