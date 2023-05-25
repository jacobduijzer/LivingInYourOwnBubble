using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Domain;

public class AnimalCategory
{
    public int Id { get; set; } 
    public Gender Gender { get; set; }
    public bool Calved { get; set; }
    public FarmType FarmType { get; set; }
    public int AgeInDays { get; set; }
    public int Category { get; set; }
}