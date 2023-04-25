using Microsoft.EntityFrameworkCore;

namespace CattleInformationSystem.Domain;

[Index(nameof(UBN), IsUnique = true)]
public class Farm
{
   public int Id { get; set; }
   public string UBN { get; set; } 
   public FarmType FarmType { get; set; }
   
   public IList<Cow> Cows { get; set; }
   public IList<FarmCow> FarmCows { get; set; }
}