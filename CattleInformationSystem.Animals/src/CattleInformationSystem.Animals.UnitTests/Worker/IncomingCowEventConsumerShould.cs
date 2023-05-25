using CattleInformationSystem.Animals.Application;
using CattleInformationSystem.Animals.Worker;
using CattleInformationSystem.SharedKernel;
using MassTransit;
using MassTransit.Context;
using MassTransit.Serialization;

namespace CattleInformationSystem.Animals.UnitTests.Worker;

public class IncomingCowEventConsumerShould
{
    [Fact]
    public void SendIncomingDataToHandler()
    {
        // TODO 
    }
}