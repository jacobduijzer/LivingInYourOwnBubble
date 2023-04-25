using System.Linq.Expressions;

namespace CIS.Domain;

public interface IGetRepository<TEntity>
{
    Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> expression);
}