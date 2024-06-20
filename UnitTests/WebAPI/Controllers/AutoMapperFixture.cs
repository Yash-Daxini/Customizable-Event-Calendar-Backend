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
            cfg.AddProfile<CollaborationRequestDtoProfile>();
            cfg.AddProfile<EventCollaboratorConfirmationDtoProfile>();
            cfg.AddProfile<EventCollaboratorRequestDtoProfile>();
            cfg.AddProfile<EventResponseDtoProfile>();
            cfg.AddProfile<NonRecurringEventRequestDtoProfile>();
            cfg.AddProfile<RecurringEventRequestDtoProfile>();
            cfg.AddProfile<RecurrencePatternDtoProfile>();
            cfg.AddProfile<SharedCalendarDtoProfile>();
            cfg.AddProfile<UserDtoProfile>();
        });

        var serviceProvider = services.BuildServiceProvider();

        Mapper = serviceProvider.GetService<IMapper>();
    }
}
