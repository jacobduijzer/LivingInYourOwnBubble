namespace CattleInformationSystem.Domain;


public class FarmCow
{
    public int Id { get; set; }
    public int FarmId { get; set; }
    public Farm Farm { get; set; }

    public int CowId { get; set; }
    public Cow Cow { get; set; }
    
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}