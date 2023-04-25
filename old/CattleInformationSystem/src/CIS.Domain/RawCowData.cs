using Microsoft.EntityFrameworkCore;

namespace CIS.Domain;

[Index(nameof(LifeNumber), IsUnique = true)]
public class RawCowData
{
    public int Id { get; set; }
    public string LifeNumber { get; set; }
    public Gender Gender { get; set; }
    public DateTime? DateCalved { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime? DateOfDeath { get; set; }
    public string? LifeNumberOfMother { get; set; }
    public List<RawCowEventData> Events { get; set; }
}