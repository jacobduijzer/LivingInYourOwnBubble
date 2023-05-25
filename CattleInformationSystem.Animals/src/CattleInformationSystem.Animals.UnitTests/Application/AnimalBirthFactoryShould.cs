using CattleInformationSystem.Animals.Application;
using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.Animals.UnitTests.Stubs;
using CattleInformationSystem.SharedKernel;
using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.UnitTests.Application;

public class AnimalBirthFactoryShould
{
    private readonly IncomingAnimalEventCreated _defaultEvent = new(
        "1000000001",
        Gender.Female,
        DateOnly.FromDateTime(DateTime.Now),
        Reason.Birth,
        FarmRepositoryStub.Farms.First().UBN,
        null,
        DateOnly.FromDateTime(DateTime.Now));
    
    [Fact]
    public async Task CreateANewAnimal()
    {
        // ARRANGE
        AnimalCategoryDeterminationService animalCategories = new(new List<AnimalCategory>());
        AnimalBirthFactory factory = new(FarmRepositoryStub.Farms, animalCategories);

        // ACT
        var animal = factory.Create(_defaultEvent);
        
        // ASSERT
        Assert.True(animal.LifeNumber.Equals("1000000001"));
        Assert.True(animal.Gender == Gender.Female);
        Assert.True(animal.DateOfBirth.Equals(DateOnly.FromDateTime(DateTime.Now)));
        Assert.False(animal.DateFirstCalved.HasValue);
        Assert.False(animal.DateOfDeath.HasValue);
        
        Assert.NotEmpty(animal.AnimalLocations);
        Assert.True(animal.AnimalLocations.Count.Equals(1));
        Assert.True(animal.AnimalLocations.First().Ubn.Equals(FarmRepositoryStub.Farms.First().UBN));
        Assert.True(animal.AnimalLocations.First().StartDate.Equals(DateOnly.FromDateTime(DateTime.Now)));
        Assert.True(animal.AnimalLocations.First().EndDate.Equals(DateOnly.MinValue));
        
        Assert.NotEmpty(animal.AnimalEvents);
        Assert.True(animal.AnimalEvents.Count.Equals(1));
        Assert.True(animal.AnimalEvents.First().Reason.Equals(Reason.Birth));
    }
}