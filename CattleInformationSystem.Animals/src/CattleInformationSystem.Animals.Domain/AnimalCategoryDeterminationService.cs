using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Animals.Domain;

public class AnimalCategoryDeterminationService
{
    private readonly IReadOnlyCollection<AnimalCategory> _categories;

    public AnimalCategoryDeterminationService(IReadOnlyCollection<AnimalCategory> categories) =>
        _categories = categories;

    public int DeterminateCurrent(Animal animal, FarmType farmType, DateOnly atDate)
    {
        var category = _categories.Where(cat =>
                cat.Gender.Equals(animal.Gender) &&
                cat.Calved.Equals(animal.DateFirstCalved.HasValue) &&
                cat.FarmType.Equals(farmType) &&
                cat.AgeInDays <= animal.AgeInDays(atDate))
            .MaxBy(cat => cat.AgeInDays);

        return category?.Category ?? 0;
    }
}