using CattleInformationSystem.SharedKernel;

namespace CattleInformationSystem.Animals.Domain;

public record struct AnimalEvent(
    string Ubn, 
    Reason Reason, 
    DateOnly EventDate, 
    int Category, 
    int Order);