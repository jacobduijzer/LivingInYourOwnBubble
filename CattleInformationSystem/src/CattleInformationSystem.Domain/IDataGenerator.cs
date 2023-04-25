namespace CattleInformationSystem.Domain;

public interface IDataGenerator
{
    List<Cow> Cows();
    List<Farm> Farms();
}