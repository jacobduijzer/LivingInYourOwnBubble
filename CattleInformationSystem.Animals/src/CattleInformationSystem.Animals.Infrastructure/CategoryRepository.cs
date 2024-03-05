using CattleInformationSystem.Animals.Domain;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Animals.Infrastructure;

public class CategoryRepository(DatabaseContext databaseContext) : ICategoryRepository
{
    public async Task<IReadOnlyCollection<AnimalCategory>> All() =>
        await databaseContext.AnimalCategories.ToListAsync();
}