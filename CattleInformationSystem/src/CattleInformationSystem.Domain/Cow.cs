using CattleInformationSystem.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Domain;

[Index(nameof(Id), nameof(LifeNumber), IsUnique = true)]
public class Cow
{
    public int Id { get; set; }
    public string LifeNumber { get; set; }
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateOnly? DateFirstCalved { get; set; }
    public DateOnly? DateOfDeath { get; set; }
    public IList<CowEvent> CowEvents { get; set; }
    public IList<Farm> Farms { get; set; }
    public IList<FarmCow> FarmCows { get; set; }
}