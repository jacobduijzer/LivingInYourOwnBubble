using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Animals.Domain;

public class AnimalCategoryDeterminationService(IEnumerable<AnimalCategory> categories)
{
    public int DeterminateCurrent(Animal animal, FarmType farmType, DateOnly eventDate)
    {
        var category = categories.Where(cat =>
                cat.Gender.Equals(animal.Gender) &&
                cat.Calved.Equals(animal.DateFirstCalved.HasValue) &&
                cat.FarmType.Equals(farmType) &&
                cat.AgeInDays <= animal.AgeInDays(eventDate))
            .MaxBy(cat => cat.AgeInDays);
        
        return category?.Category ?? 0;
    }
}