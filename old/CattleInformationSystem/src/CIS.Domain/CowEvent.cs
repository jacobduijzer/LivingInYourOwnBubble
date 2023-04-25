namespace CIS.Domain;

public class CowEvent
{
    public int Id { get; set; }
    public DateTime OccuredAt { get; set; }
    public AnimalEventType Reason { get; set; } 
    public int Order { get; set; }
    
    public int CowId { get; set; }
    public Cow Cow { get; set; }
    
    public int? FarmLocationId { get; set; }
    public FarmLocation? FarmLocation { get; set; }
    
    public int AnimalCategoryId { get; set; }
    public AnimalCategory AnimalCategory { get; set; }
}