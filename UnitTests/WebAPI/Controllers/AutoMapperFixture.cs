using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Profiles;

namespace UnitTests.WebAPI.Controllers;

public class AutoMapperFixture
{
    public IMapper Mapper { get; private set; }

    public AutoMapperFixture()
    {
        var services = new ServiceCollection();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<DurationDtoProfile>();
            cfg.AddProfile<EventCollaborationRequestDtoProfile>();
            cfg.AddProfile<EventCollaboratorConfirmationDtoProfile>();
            cfg.AddProfile<EventCollaboratorRequestDtoProfile>();
            cfg.AddProfile<EventCollaboratorResponseDtoProfile>();
            cfg.AddProfile<EventResponseDtoProfile>();
            cfg.AddProfile<NonRecurringEventRequestDtoProfile>();
            cfg.AddProfile<RecurrencePatternDtoProfile>();
            cfg.AddProfile<RecurringEventRequestDtoProfile>();
            cfg.AddProfile<SharedCalendarDtoProfile>();
            cfg.AddProfile<UserDtoProfile>();
        }, typeof(NonRecurringEventRecurrencePatternResolver).Assembly,
           typeof(RecurringEventDateWiseEventCollaboratorsResolver).Assembly,
           typeof(NonRecurringEventDateWiseEventCollaboratorsResolver).Assembly);

        var serviceProvider = services.BuildServiceProvider();

        Mapper = serviceProvider.GetService<IMapper>();
    }
}
