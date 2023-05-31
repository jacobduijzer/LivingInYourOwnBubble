using System.ComponentModel.DataAnnotations.Schema;
using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Animals.Infrastructure.AnimalCowMapping;

[Table("CowEvents")]
public class CowEvent
{
    public int Id { get; set; }
    public int FarmId { get; set; }
    public int CowId { get; set; }
    public Reason Reason { get; set; }
    public int Category { get; set; }
    public int Order { get; set; }
    public DateOnly EventDate { get; set; } 
}