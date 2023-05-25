namespace CattleInformationSystem.SharedKernel.Contracts;

public record IncomingAnimalEventCreated(
    string LifeNumber,
    Gender Gender,
    DateOnly DateOfBirth,
    Reason Reason,
    string CurrentUbn,
    string? TargetUbn,
    DateOnly EventDate);