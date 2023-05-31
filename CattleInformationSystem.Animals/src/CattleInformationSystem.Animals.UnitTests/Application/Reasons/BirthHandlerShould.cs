using CattleInformationSystem.Animals.Application.Reasons;
using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.Animals.Infrastructure;
using CattleInformationSystem.Animals.Infrastructure.AnimalCowMapping;
using CattleInformationSystem.Animals.UnitTests.Stubs;
using CattleInformationSystem.SharedKernel;
using CattleInformationSystem.SharedKernel.Contracts;
using Moq;

namespace CattleInformationSystem.Animals.UnitTests.Application.Reasons;

public class BirthHandlerShould
{
    [Fact]
    public async Task CreateAnimalAndSaveAsCowToLegacySystem()
    {
        // ARRANGE
        var cowMock = new Mock<ICowRepository>();
        var farmRepository = new FarmRepositoryStub();
        AnimalAcl animalAcl = new(cowMock.Object, farmRepository);
        var farms = await farmRepository.ByUbns(new[] {"3000000001", "3000000002"});

        BirthHandler birthHandler = new(animalAcl, farms, new AnimalCategoryDeterminationService(await new CategoryRepositoryStub().All()));

        // ACT
        await birthHandler.Handle(new IncomingAnimalEventCreated(
            "123456789", 
            Gender.Female,
            new DateOnly(2020, 2, 2), 
            Reason.Birth, 
            "3000000001", 
            null,
            DateOnly.FromDateTime(DateTime.Now)));

        // ASSERT
        cowMock.Verify(x => x.Save(It.Is<Cow>(cow => 
            cow.LifeNumber.Equals("123456789") &&
            cow.DateOfBirth.Equals(new DateOnly(2020, 2, 2)) &&
            cow.Gender.Equals(Gender.Female) &&
            cow.CowEvents.First().Category.Equals(101) &&
            cow.CowEvents.First().FarmId.Equals(1) &&
            cow.CowEvents.First().Reason.Equals(Reason.Birth))), Times.Once);
    }
}