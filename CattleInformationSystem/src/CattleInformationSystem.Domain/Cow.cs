using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Domain;

[Index(nameof(Id), nameof(LifeNumber), IsUnique = true)]
public class Cow
{
    public int Id { get; set; }
    public string LifeNumber { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime? DateFirstCalved { get; set; }
    public DateTime? DateOfDeath { get; set; }
    public IList<Farm> Farms { get; set; }
    public IList<FarmCow> FarmCows { get; set; }
}