namespace CattleInformationSystem.Animals.Domain;

public record struct AnimalLocation(string Ubn, DateOnly StartDate)
{
    public DateOnly? EndDate { get; private set; }

    public void SetEndDate(DateOnly endDate) => EndDate = endDate;
}