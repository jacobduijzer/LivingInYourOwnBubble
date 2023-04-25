using System.Text.Json.Serialization;
using CIS.Domain;

namespace CIS.Application;

public class CowEventDto
{
    public string LocationNumber { get; set; }
    public DateTime OccuredAt { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AnimalEventType Reason { get; set; }
}