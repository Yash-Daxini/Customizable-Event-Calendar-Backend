using AutoMapper;
using Infrastructure.Profiles;

namespace UnitTests.Infrastructure.Repositories;

public class AutoMapperFixture
{
    public IMapper Mapper { get; private set; }

    public AutoMapperFixture()
    {
        Mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<EventProfile>();
            cfg.AddProfile<EventCollaboratorProfile>();
            cfg.AddProfile<SharedCalendarProfile>();
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<SingleInstanceRecurrencePatternProfile>();
            cfg.AddProfile<DailyRecurrencePatternProfile>();
            cfg.AddProfile<WeeklyRecurrencePatternProfile>();
            cfg.AddProfile<MonthlyRecurrencePatternProfile>();
            cfg.AddProfile<YearlyRecurrencePatternProfile>();
        }).CreateMapper();
    }
}
