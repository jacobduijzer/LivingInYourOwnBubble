using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Animals.Domain;

public class Farm : IAggregateRoot
{
    public int Id { get; set; }
    public string UBN { get; set; } 
    public FarmType FarmType { get; set; }
}