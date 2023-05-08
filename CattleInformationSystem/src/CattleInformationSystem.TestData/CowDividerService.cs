using CattleInformationSystem.Domain;

namespace CattleInformationSystem.TestData;

public class CowDividerService
{
    private readonly Random _random;
    private List<Farm> _farms;

    public CowDividerService()
    {
        _random = new Random();
    }
    public void DivideCows(List<Cow> cows)
    {
        Stack<Cow> femaleStack = new Stack<Cow>(cows.Where(cow => cow.Gender == Gender.Female));
        Stack<Cow> maleStack = new Stack<Cow>(cows.Where(cow => cow.Gender == Gender.Male));
    
        // female
        var totalFemale = cows.Count(cow => cow.Gender == Gender.Female);
        
        // 10% milk (or, > 2 && < 15%)
        var femaleAmountBornOnMilk = (int)Math.Floor(totalFemale * (_random.Next(2, 15) / 100.0));
        

        // 10% breadingformeat (or > 2 && < 15%)
        var femaleAmountBornOnForMeat = (int)Math.Floor(totalFemale * (_random.Next(2, 15) / 100.0));
        
        // 80% breadingformilk (or remaining)
        var femaleAmountBornOnBreedForMilk = totalFemale - femaleAmountBornOnMilk - femaleAmountBornOnForMeat;

        // male
        var totalMale = cows.Count(cow => cow.Gender == Gender.Male);
        
        // 10% milk (or, > 2 && < 15%)
        var maleAmountBornOnMilk = (int)Math.Floor(totalMale * (_random.Next(2, 15) / 100.0));

        // 10% breadingformeat (or > 2 && < 15%)
        var maleAmountBornOnForMeat = (int)Math.Floor(totalMale * (_random.Next(2, 15) / 100.0));
        
        // 80% breadingformilk (or remaining)
        var maleAmountBornOnBreedForMilk = totalMale - maleAmountBornOnMilk - maleAmountBornOnForMeat;
        
        // female, born on milk
        
        

    }
}