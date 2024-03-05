using CattleInformationSystem.Animals.Application;
using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.Animals.UnitTests.Stubs;
using CattleInformationSystem.SharedKernel;
using CattleInformationSystem.SharedKernel.Contracts;
using Moq;

namespace CattleInformationSystem.Animals.UnitTests.Application;

public class IncomingCowEventHandlerShould
{
    private readonly IncomingAnimalEventCreated _defaultEvent = new IncomingAnimalEventCreated(
        "12345",
        Gender.Female,
        new DateOnly(2017, 1, 1),
        Reason.Birth,
        "3000000001",
        null,
        new DateOnly(2017, 1, 1));
    
    [Fact]
    public void RequestTheCorrectCategoriesViaAclWithCurrentUbnOnly()
    {
       // ARRANGE
       var mockAnimals = new Mock<IAnimalAcl>();
       var mockFarms = new Mock<IFarmRepository>();
       IncomingCowEventHandler handler = new(mockAnimals.Object, new CategoryRepositoryStub(), mockFarms.Object);

       // ACT
       _ = handler.Handle(_defaultEvent);

       // ASSERT
       mockFarms.Verify(x => x.ByUbns(new []{ _defaultEvent.CurrentUbn}), Times.Once);
    }
    
    [Fact]
    public void RequestTheCorrectCategoriesViaAclWithCurrentAndDestinationUbn()
    {
        // ARRANGE
        var mockAnimals = new Mock<IAnimalAcl>();
        var mockFarms = new Mock<IFarmRepository>();
        IncomingCowEventHandler handler = new(mockAnimals.Object, new CategoryRepositoryStub(), mockFarms.Object);

        // ACT
        _ = handler.Handle(_defaultEvent with { TargetUbn = "3000000003" });

        // ASSERT
        mockFarms.Verify(x => x.ByUbns(new []{ _defaultEvent.CurrentUbn, "3000000003" }), Times.Once);
    }
}