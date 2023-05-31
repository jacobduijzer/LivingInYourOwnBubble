using CattleInformationSystem.Animals.Application;
using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.Animals.UnitTests.Stubs;
using CattleInformationSystem.SharedKernel;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.UnitTests.Domain;

public class AnimalShould
{
    [Fact]
    public async Task HandleDeath()
    {
        // ARRANGE
        AnimalCategoryDeterminationService animalCategories = new(await new CategoryRepositoryStub().All());
        var animal = new AnimalBirthFactory(FarmRepositoryStub.Farms, animalCategories)
            .PerformBirth(new IncomingAnimalEventCreated("123456", Gender.Female, new DateOnly(2012, 1, 1), Reason.Birth,
                "3000000001", null, new DateOnly(2012, 1, 1)));

        // ACT
        animal.HandleDeath("3000000001", new DateOnly(2012, 1, 1));

        // ASSERT
        Assert.True(animal.DateOfDeath.HasValue);
        Assert.True(animal.DateOfDeath.Value.Equals(new DateOnly(2012, 1, 1)));
        Assert.True(animal.AnimalEvents.Count.Equals(2));
        Assert.True(animal.AnimalEvents.First(x => x.Reason.Equals(Reason.Death)).EventDate.Equals(new DateOnly(2012, 1, 1)));
    }
}