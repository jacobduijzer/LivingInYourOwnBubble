using CattleInformationSystem.Animals.Application.Reasons;
using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.Animals.Infrastructure;
using CattleInformationSystem.Animals.Infrastructure.AnimalCowMapping;
using CattleInformationSystem.Animals.UnitTests.Stubs;
using CattleInformationSystem.SharedKernel;
using CattleInformationSystem.SharedKernel.Contracts;
using Moq;

namespace CattleInformationSystem.Animals.UnitTests.Application.Reasons;

public class DepartureHandlerShould
{
    [Fact]
    public async Task CreateDepartureAndArrivalEventAndUpdateCowInLegacySystem()
    {
        // ARRANGE
        var lifeNumber = "123456789";
        var dateOfBirth = new DateOnly(2017, 5, 1);
        var eventDate = new DateOnly(2018, 07, 25);

        var cowMock = SetupCowMock(lifeNumber, dateOfBirth);
        var farmRepository = new FarmRepositoryStub();
        AnimalAcl animalAcl = new(cowMock.Object, farmRepository);
        var farms = await farmRepository.ByUbns(new[] {"3000000001", "3000000003"});

        DepartureHandler departureHandler = new(animalAcl, farms,
            new AnimalCategoryDeterminationService(await new CategoryRepositoryStub().All()));

        // ACT
        await departureHandler.Handle(new IncomingAnimalEventCreated(
            lifeNumber,
            Gender.Female,
            dateOfBirth,
            Reason.Departure,
            "3000000001",
            "3000000003",
            eventDate));

        // ASSERT
        cowMock.Verify(x => x.Update(It.Is<Cow>(cow =>
            cow.LifeNumber.Equals(lifeNumber) &&
            cow.DateOfBirth.Equals(dateOfBirth) &&
            cow.CowEvents.Count().Equals(3) &&

            // event 1
            cow.CowEvents.ToArray()[0].Reason.Equals(Reason.Birth) &&
            cow.CowEvents.ToArray()[0].Category.Equals(101) &&
            cow.CowEvents.ToArray()[0].EventDate.Equals(dateOfBirth) &&
            cow.CowEvents.ToArray()[0].Order.Equals(0) &&

            // event 2
            cow.CowEvents.ToArray()[1].Reason.Equals(Reason.Departure) &&
            cow.CowEvents.ToArray()[1].Category.Equals(101) &&
            cow.CowEvents.ToArray()[1].EventDate.Equals(eventDate) &&
            cow.CowEvents.ToArray()[1].Order.Equals(0) &&

            // event 3
            cow.CowEvents.ToArray()[2].Reason.Equals(Reason.Arrival) &&
            cow.CowEvents.ToArray()[2].Category.Equals(102) &&
            cow.CowEvents.ToArray()[2].EventDate.Equals(eventDate) &&
            cow.CowEvents.ToArray()[2].Order.Equals(0) &&

            // location 1
            cow.FarmCows.First().StartDate.Equals(dateOfBirth) &&
            cow.FarmCows.First().EndDate.Equals(eventDate) &&

            // location 2
            cow.FarmCows.Last().StartDate.Equals(eventDate) &&
            !cow.FarmCows.Last().EndDate.HasValue)), Times.Once());
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