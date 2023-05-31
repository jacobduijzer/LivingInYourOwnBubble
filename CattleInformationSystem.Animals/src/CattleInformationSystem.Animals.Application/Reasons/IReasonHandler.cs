using CattleInformationSystem.SharedKernel.Contracts;

namespace CattleInformationSystem.Animals.Application.Reasons;

public interface IReasonHandler
{
    Task Handle(IncomingAnimalEventCreated incomingAnimalEvent);
}