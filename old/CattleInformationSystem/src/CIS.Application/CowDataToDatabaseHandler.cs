using CIS.Domain;
using CIS.Infrastructure;

namespace CIS.Application;

public class CowDataToDatabaseHandler
{
    public record Command(CowDto CowDto);

    public class Handler
    {
        private readonly RawCowDataRepository _rawCowDataRepository;

        public Handler(RawCowDataRepository rawCowDataRepository)
        {
            _rawCowDataRepository = rawCowDataRepository;
        }

        public async Task Handle(Command command)
        {
            var cow = new RawCowData
            {
                LifeNumber = command.CowDto.LifeNumber,
                Gender = command.CowDto.Gender,
                DateCalved = command.CowDto.DateCalved,
                DateOfBirth = command.CowDto.DateOfBirth,
                DateOfDeath = command.CowDto.DateOfDeath,
                LifeNumberOfMother = command.CowDto.LifeNumberOfMother,
                Events = new List<RawCowEventData>()
            };

            foreach (var rawEvent in command.CowDto.Events)
            {
                cow.Events.Add(new()
                {
                    LocationNumber = rawEvent.LocationNumber,
                    OccuredAt = rawEvent.OccuredAt,
                    Reason = rawEvent.Reason,
                });
            }

            await _rawCowDataRepository.InsertRawCow(cow);
        }
    }
}