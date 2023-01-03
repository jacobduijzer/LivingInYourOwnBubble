using Microsoft.EntityFrameworkCore;

namespace CIS.Domain;

[Keyless]
public class RawCowData
{
    public string LifeNumber { get; set; }
    public string Gender { get; set; }
}
