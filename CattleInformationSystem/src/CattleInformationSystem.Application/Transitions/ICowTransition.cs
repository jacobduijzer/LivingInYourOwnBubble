using CattleInformationSystem.Domain;

namespace CattleInformationSystem.Application.Transitions;

public interface ICowTransition
{
    Task Handle(Farm farm);
}