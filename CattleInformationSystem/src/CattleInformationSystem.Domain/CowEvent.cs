using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Domain;

public class CowEvent
{
    public int Id { get; set; }
    public int FarmId { get; set; }
    public Farm Farm { get; set; }
    public int CowId { get; set; }
    public Cow Cow { get; set; }
    public Reason Reason { get; set; }
    public int Category { get; set; }
    public int Order { get; set; }
    public DateOnly EventDate { get; set; }
}