using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application.Reasons;

public class DepartureHandler : IReasonHandler
{
    public Task Handle(IncomingAnimalEventCreated incomingAnimalEvent)
    {
        throw new NotImplementedException();
    }
}