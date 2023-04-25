namespace CIS.Domain;

public class RawCowEventData
{
    public int Id { get; set; }
    public string LocationNumber { get; set; }
    public DateTime OccuredAt { get; set; }
    public AnimalEventType Reason { get; set; }

    public int RawCowDataId { get; set; }
    public RawCowData RawCowData { get; set; }
}