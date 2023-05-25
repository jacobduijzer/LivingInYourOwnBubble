using CattleInformationSystem.Animals.Domain;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Animals.Infrastructure;

public class CategoryRepository : ICategoryRepository
{
    private readonly DatabaseContext _databaseContext;

    public CategoryRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<IReadOnlyCollection<AnimalCategory>> All() =>
        await _databaseContext.AnimalCategories.ToListAsync();
}