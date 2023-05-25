using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Animals.Domain;

public class Farm 
{
    public int Id { get; set; }
    public string UBN { get; set; } 
    public FarmType FarmType { get; set; }
}