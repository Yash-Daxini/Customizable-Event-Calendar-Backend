using AutoMapper;
using WebAPI.Profiles;

namespace UnitTests.WebAPI.Controllers;

public class AutoMapperFixture
{
    public IMapper Mapper { get; private set; }

    public AutoMapperFixture()
    {
        Mapper = new MapperConfiguration(cfg =>
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
            cfg.AddProfile<UserResponseDtoProfile>();
            cfg.AddProfile<AuthenticateRequestDtoProfile>();
            cfg.AddProfile<AuthenticateResponseDtoProfile>();
            cfg.AddProfile<UserRequestDtoProfile>();
        }).CreateMapper();
    }
}
