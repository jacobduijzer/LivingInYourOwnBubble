namespace CIS.Domain;

public record AnimalCategory(
    int Id,
    Gender Gender,
    bool Calved,
    ProductionTarget ProductionTarget,
    int AgeInDays,
    int AgeInMonths,
    int AgeInYears,
    int Category
);
//public class AnimalCategory
//{
//    public int Id { get; set; }
//    public Gender Gender { get; set; }
//    public ProductionTarget ProductionTarget { get; set; }
//    public int AgeInDays { get; set; }
//    public int AgeInMonths { get; set; }
//    public int AgeInYears { get; set; }
//    public int Category { get; set; }
//}