namespace CattleInformationSystem.Domain;

public interface IIncomingCowEventRepository
{
    Task Add(IncomingCowEvent incomingCowEvent);

    Task<List<IncomingCowEvent>> ForLifeNumber(string lifeNumber);
}