namespace CIS.Domain;

public class FarmLocation
{
    public int Id { get; set; }
    public string LocationNumber { get; set; }
    public ProductionTarget ProductionTarget { get; set; }
}