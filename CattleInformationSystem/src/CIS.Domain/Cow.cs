using Microsoft.EntityFrameworkCore;

namespace CIS.Domain;

[Index(nameof(LifeNumber), IsUnique = true)]
public class Cow
{
    public int Id { get; set; }
    public string LifeNumber { get; set; }
    public string Gender { get; set; }
}