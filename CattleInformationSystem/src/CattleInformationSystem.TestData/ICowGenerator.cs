using CattleInformationSystem.Domain;

namespace CattleInformationSystem.TestData;

public interface ICowGenerator
{
    List<Cow> Generate(int numberOfCows);
}