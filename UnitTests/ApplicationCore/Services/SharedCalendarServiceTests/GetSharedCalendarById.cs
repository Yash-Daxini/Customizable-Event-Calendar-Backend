using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace UnitTests.ApplicationCore.Services.SharedCalendarServiceTests;

public class GetSharedCalendarById
{

    private readonly ISharedCalendarService _sharedCalendarService;

    private readonly ISharedCalendarRepository _sharedCalendarRepository;

    public GetSharedCalendarById()
    {
        _sharedCalendarRepository = Substitute.For<ISharedCalendarRepository>();
        _sharedCalendarService = new SharedCalendarService(_sharedCalendarRepository);
    }

    [Fact]
    public async Task Should_ReturnsSharedCalendar_When_IdWithSharedCalendarAvailable()
    {
        SharedCalendar sharedCalendar = new()
        {
            Id = 1,
            Sender = new()
            {
                Id = 1,
                Name = "1",
                Email = "x@gmail.com",
                Password = "1",

            },
            Receiver = new()
            {
                Id = 2,
                Name = "2",
                Email = "y@gmail.com",
                Password = "2",
            },
            FromDate = new DateOnly(2024, 6, 2),
            ToDate = new DateOnly(2024, 6, 20)
        };

        _sharedCalendarRepository.GetSharedCalendarById(1).Returns(sharedCalendar);

        SharedCalendar actualOutput = await _sharedCalendarService.GetSharedCalendarById(1);

        _sharedCalendarRepository.Received().GetSharedCalendarById(1);

        Assert.Equal(sharedCalendar, actualOutput);
    }

    [Fact]
    public async Task Should_ReturnsNull_When_IdWithSharedCalendarNotAvailable()
    {
        _sharedCalendarRepository.GetSharedCalendarById(1).ReturnsNull();

        await Assert.ThrowsAsync<NotFoundException>(() =>_sharedCalendarService.GetSharedCalendarById(1));

        _sharedCalendarRepository.Received().GetSharedCalendarById(1);
    }
}
