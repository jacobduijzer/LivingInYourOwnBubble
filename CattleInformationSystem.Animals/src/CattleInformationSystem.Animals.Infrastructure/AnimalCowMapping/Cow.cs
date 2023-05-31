using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Animals.Infrastructure.AnimalCowMapping;

public class Cow
{
    public int Id { get; set; }
    public string LifeNumber { get; set; }
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateOnly? DateFirstCalved { get; set; }
    public DateOnly? DateOfDeath { get; set; }
    public IList<CowEvent> CowEvents { get; set; }
    public IList<FarmCow> FarmCows { get; set; }
}