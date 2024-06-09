using AutoMapper;
using Infrastructure.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.Infrastructure.Repositories;

public class AutoMapperFixture
{
    public IMapper Mapper { get; private set; }

    public AutoMapperFixture()
    {
        var services = new ServiceCollection();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<EventProfile>();
            cfg.AddProfile<EventCollaboratorProfile>();
            cfg.AddProfile<SharedCalendarProfile>();
            cfg.AddProfile<UserProfile>();
        },typeof(DateWiseEventCollaboratorsResolver).Assembly,
          typeof(EventCollaboratorResolver).Assembly);

        var serviceProvider = services.BuildServiceProvider();

        Mapper = serviceProvider.GetService<IMapper>();
    }
}
