namespace CattleInformationSystem.Domain;

public class IncomingCowEvent
{
    public int Id { get; set; }
    public string LifeNumber { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string UBN_1 { get; set; } 
    public string? UBN_2 { get; set; } 
    public Reason Reason { get; set; }
    public DateTime EventDate { get; set; }
}