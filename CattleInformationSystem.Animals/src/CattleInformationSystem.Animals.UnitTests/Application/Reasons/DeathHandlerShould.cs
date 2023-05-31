using CattleInformationSystem.Animals.Application.Reasons;
using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.Animals.Infrastructure;
using CattleInformationSystem.Animals.Infrastructure.AnimalCowMapping;
using CattleInformationSystem.Animals.UnitTests.Stubs;
using CattleInformationSystem.SharedKernel;
using CattleInformationSystem.SharedKernel.Contracts;
using Moq;

namespace CattleInformationSystem.Animals.UnitTests.Application.Reasons;

public class DeathHandlerShould
{
    [Fact]
    public async Task UpdateAnimalAndUpdateCowInLegacySystem()
    {
        // ARRANGE
        var lifeNumber = "123456789";
        var dateOfBirth = new DateOnly(2020, 2, 2);

        var cowMock = SetupCowMock(lifeNumber, dateOfBirth);
        var farmRepository = new FarmRepositoryStub();
        AnimalAcl animalAcl = new(cowMock.Object, farmRepository);
        var farms = await farmRepository.ByUbns(new[] {"3000000001", "3000000002"});

        DeathHandler deathHandler = new(animalAcl, farms);

        // ACT
        await deathHandler.Handle(new IncomingAnimalEventCreated(
            lifeNumber, 
            Gender.Female,
            dateOfBirth, 
            Reason.Death, 
            "3000000001", 
            null,
            DateOnly.FromDateTime(DateTime.Now)));

        // ASSERT
        cowMock.Verify(x => x.Update(It.Is<Cow>(cow => 
            cow.LifeNumber.Equals(lifeNumber) &&
            cow.DateOfBirth.Equals(dateOfBirth) &&
            cow.DateOfDeath.Equals(DateOnly.FromDateTime(DateTime.Now)) &&
            cow.CowEvents.Count().Equals(2) &&
            cow.CowEvents.First(y => y.Reason.Equals(Reason.Death)).Category.Equals(101) &&
            cow.FarmCows.First().StartDate.Equals(dateOfBirth) &&
            cow.FarmCows.First().EndDate.Equals(DateOnly.FromDateTime(DateTime.Now)))), Times.Once());
    }

    private Mock<ICowRepository> SetupCowMock(
        string lifeNumber, 
        DateOnly dateOfBirth)
    {
        var cowRepoStub = new CowRepositoryStub();
        cowRepoStub.SetCow(new Cow
        {
            LifeNumber = lifeNumber,
            DateOfBirth = dateOfBirth,
            CowEvents = new List<CowEvent>
            {
                new CowEvent
                {
                   FarmId = 1,
                   Category = 101,
                   Reason = Reason.Birth,
                   EventDate = dateOfBirth,
                   Order = 0
                }
            },
            FarmCows = new List<FarmCow>
            {
                new FarmCow
                {
                    FarmId = 1,
                    StartDate = dateOfBirth
                }
            }
        });
        
        var cowMock = new Mock<ICowRepository>();
        cowMock
            .Setup(x => x.ByLifeNumber(lifeNumber))
            .Returns(async () => await cowRepoStub.ByLifeNumber(lifeNumber));

        return cowMock;
    }
}