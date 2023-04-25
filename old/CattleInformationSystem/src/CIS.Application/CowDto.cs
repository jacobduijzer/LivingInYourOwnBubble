using System.Text.Json.Serialization;
using CIS.Domain;

namespace CIS.Application;

public class CowDto
{
    public string LifeNumber { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender Gender { get; set; }
    public DateTime? DateCalved { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime? DateOfDeath { get; set; }
    public string? LifeNumberOfMother { get; set; }
    public List<CowEventDto> Events { get; set; }
}