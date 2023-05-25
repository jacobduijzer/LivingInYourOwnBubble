using System.ComponentModel.DataAnnotations.Schema;
using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Animals.Infrastructure.AnimalCowMapping;

[Table("FarmCows")]
public class FarmCow
{
    public int Id { get; set; }
    public int FarmId { get; set; }
    public int CowId { get; set; }
    
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}