using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using FluentAssertions;
using NSubstitute;

namespace UnitTests.ApplicationCore.Services.SharedCalendarServiceTests;

public class GetAllSharedCalendars
{

    private readonly ISharedCalendarService _sharedCalendarService;

    private readonly ISharedCalendarRepository _sharedCalendarRepository;

    private readonly List<SharedCalendar> _sharedCalendars;

    public GetAllSharedCalendars()
    {
        _sharedCalendarRepository = Substitute.For<ISharedCalendarRepository>();
        _sharedCalendarService = new SharedCalendarService(_sharedCalendarRepository);
        _sharedCalendars =
            [
                new(1,
                    new(){Id = 1,Name = "1",Email = "x@gmail.com",Password = "1" },
                    new(){Id = 2,Name = "2",Email = "y@gmail.com",Password = "2" },
                    new DateOnly(2024,6,2),
                    new DateOnly(2024,6,20)),
                new(2,
                    new(){Id = 2,Name = "2",Email = "y@gmail.com",Password = "2" },
                    new(){Id = 1,Name = "1",Email = "x@gmail.com",Password = "1" },
                    new DateOnly(2024,6,12),
                    new DateOnly(2024,6,22))
            ];
    }

    [Fact]
    public async Task Should_Return_ListOfSharedCalendars_When_SharedCalendarsAvailable()
    {
        _sharedCalendarRepository.GetAllSharedCalendars().Returns(_sharedCalendars);

        List<SharedCalendar> sharedCalendars = await _sharedCalendarService.GetAllSharedCalendars();

        await _sharedCalendarRepository.Received().GetAllSharedCalendars();

        sharedCalendars.Should().BeEquivalentTo(_sharedCalendars);
    }
}
