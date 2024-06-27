using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using FluentAssertions;
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
    public async Task Should_Return_SharedCalendar_When_SharedCalendarAvailableWithId()
    {
        SharedCalendar expectedResult = new(1,
                    new User { Id = 1, Name = "1", Email = "x@gmail.com", Password = "1" },
                    new User { Id = 2, Name = "2", Email = "y@gmail.com", Password = "2" },
                    new DateOnly(2024, 6, 2),
                    new DateOnly(2024, 6, 20));

        _sharedCalendarRepository.GetSharedCalendarById(1).Returns(expectedResult);

        SharedCalendar? actualResult = await _sharedCalendarService.GetSharedCalendarById(1);

        await _sharedCalendarRepository.Received().GetSharedCalendarById(1);

        actualResult.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_Throw_NotFoundException_When_SharedCalendarNotAvailableWithId()
    {
        _sharedCalendarRepository.GetSharedCalendarById(1).ReturnsNull();

        var action = async () => await _sharedCalendarService.GetSharedCalendarById(1);

        await action.Should().ThrowAsync<NotFoundException>();

        await _sharedCalendarRepository.Received().GetSharedCalendarById(1);
    }

    [Fact]
    public async Task Should_Throw_NotFoundException_When_IdWithSharedCalendarNotValid()
    {
        _sharedCalendarRepository.GetSharedCalendarById(-11).ReturnsNull();

        var action = async () => await _sharedCalendarService.GetSharedCalendarById(-11);

        await action.Should().ThrowAsync<NotFoundException>();

        await _sharedCalendarRepository.DidNotReceive().GetSharedCalendarById(1);
    }
}
