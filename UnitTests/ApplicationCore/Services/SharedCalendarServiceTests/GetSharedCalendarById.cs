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
        SharedCalendar sharedCalendar = new(1,
                    new(1, "1", "x@gmail.com", "1"),
                    new(2, "2", "y@gmail.com", "2"),
                    new DateOnly(2024, 6, 2),
                    new DateOnly(2024, 6, 20));

        _sharedCalendarRepository.GetSharedCalendarById(1).Returns(sharedCalendar);

        SharedCalendar actualOutput = await _sharedCalendarService.GetSharedCalendarById(1);

        _sharedCalendarRepository.Received().GetSharedCalendarById(1);

        Assert.Equal(sharedCalendar, actualOutput);
    }

    [Fact]
    public async Task Should_ThrowException_When_IdWithSharedCalendarNotAvailable()
    {
        _sharedCalendarRepository.GetSharedCalendarById(1).ReturnsNull();

        await Assert.ThrowsAsync<NotFoundException>(() => _sharedCalendarService.GetSharedCalendarById(1));

        _sharedCalendarRepository.Received().GetSharedCalendarById(1);
    }

    [Fact]
    public async Task Should_ThrowException_When_IdWithSharedCalendarNotValid()
    {
        _sharedCalendarRepository.GetSharedCalendarById(-11).ReturnsNull();

        await Assert.ThrowsAsync<ArgumentException>(() => _sharedCalendarService.GetSharedCalendarById(-1));

        await _sharedCalendarRepository.DidNotReceive().GetSharedCalendarById(1);
    }
}
