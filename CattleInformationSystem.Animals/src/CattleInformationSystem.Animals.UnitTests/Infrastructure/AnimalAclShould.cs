using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.Animals.Infrastructure;
using CattleInformationSystem.Animals.Infrastructure.AnimalCowMapping;
using CattleInformationSystem.Animals.UnitTests.Stubs;
using CattleInformationSystem.SharedKernel;
using Moq;

namespace CattleInformationSystem.Animals.UnitTests.Infrastructure;

public class AnimalAclShould
{
    [Fact]
    public async Task ConvertLegacyCowFromDatabaseToAnimal()
    {
        // ARRANGE
        var cows = new CowRepositoryStub();
        var farms = new FarmRepositoryStub();
            
        AnimalAcl animalAcl = new(cows, farms);
        
        // ACT
        var animal = await animalAcl.ByLifeNumber("123456789");

        // ASSERT
        Assert.IsType<Animal>(animal);
    }

    [Fact]
    public async Task ConvertAnimalToLegacyCow()
    {
        // ARRANGE
        var cowMock = new Mock<ICowRepository>();
        var farms = new FarmRepositoryStub();
        AnimalAcl animalAcl = new(cowMock.Object, farms);
        Animal animal = Animal.CreateNew("123456789", Gender.Female, DateOnly.FromDateTime(DateTime.Now));
        
        // ACT
        await animalAcl.Save(animal);

        // ASSERT
        cowMock.Verify(x => x.Save(It.Is<Cow>(y => y.LifeNumber.Equals("123456789"))), Times.Once);
    }

    [Fact]
    public async Task UpdateLegacyCowFromAnimal()
    {
        // ARRANGE
        var lifeNumber = "123456789";
        var cowMock = new Mock<ICowRepository>();
        cowMock
            .Setup(x => x.ByLifeNumber(lifeNumber))
            .Returns(async () => await new CowRepositoryStub().ByLifeNumber(lifeNumber));
        var farms = new FarmRepositoryStub();
        AnimalAcl animalAcl = new(cowMock.Object, farms);
        Animal animal = Animal.CreateNew(lifeNumber, Gender.Female, DateOnly.FromDateTime(DateTime.Now));
        
        // ACT
        await animalAcl.Update(animal);

        // ASSERT
        cowMock.Verify(x => x.Update(It.Is<Cow>(y => y.LifeNumber.Equals(lifeNumber))), Times.Once);
    }
    
}