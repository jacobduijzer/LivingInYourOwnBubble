using CattleInformationSystem.Animals.Application.Reasons;
using CattleInformationSystem.Animals.Domain;
using CattleInformationSystem.SharedKernel;
using CattleInformationSystem.SharedKernel.Contracts;
using Moq;

namespace CattleInformationSystem.Animals.UnitTests.Application.Reasons;

public class ReasonHandlerFactoryShould
{
    [Fact] 
    public void ThrowUnimplementedWhenAnimalEventIsArrival()
    {
        // ARRANGE
        var animalAclMock = new Mock<IAnimalAcl>();
        ReasonHandlerFactory factory = new(
            new List<AnimalCategory>(),
            new List<Farm>(),
            animalAclMock.Object);
        IncomingAnimalEventCreated animalEvent = new(
            "1234566", 
            Gender.Female, 
            DateOnly.MinValue, 
            Reason.Arrival,
            "1234567", 
            null, 
            DateOnly.MinValue);

        // ACT
        var exception = Record.Exception(() => factory.CreateHandler(animalEvent));
        
        // ASSERT
        Assert.IsType<NotImplementedException>(exception);
        Assert.True(exception.Message.Equals("A handler for reason 'Arrival' is not implemented."));
    }
    
    [Theory]
    [InlineData(Reason.Birth, typeof(BirthHandler))]
    [InlineData(Reason.Departure, typeof(DepartureHandler))]
    [InlineData(Reason.Calved, typeof(CalvedHandler))]
    [InlineData(Reason.Death, typeof(DeathHandler))]
    public void CreateHandlerForImplementedTypes(Reason reason, Type expectedType)
    {
        // ARRANGE
        var animalAclMock = new Mock<IAnimalAcl>();
        ReasonHandlerFactory factory = new(
            new List<AnimalCategory>(),
            new List<Farm>(),
            animalAclMock.Object);
        IncomingAnimalEventCreated animalEvent = new(
            "1234566", 
            Gender.Female, 
            DateOnly.MinValue, 
            reason,
            "1234567", 
            null, 
            DateOnly.MinValue);

        // ACT
        IReasonHandler handler = factory.CreateHandler(animalEvent);
        
        // ASSERT
        Assert.NotNull(handler);
        Assert.IsType(expectedType, handler);
    }
}