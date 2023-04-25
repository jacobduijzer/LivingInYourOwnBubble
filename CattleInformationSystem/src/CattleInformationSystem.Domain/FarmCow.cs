namespace CattleInformationSystem.Domain;

public class FarmCow
{
    public int FarmId { get; set; }
    public Farm Farm { get; set; }

    public int CowId { get; set; }
    public Cow Cow { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}