using System.Text;
using Core.Entities;
using Core.Entities.Enums;
using Core.Interfaces.IServices;
using Core.Services;
using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Services.OverlapEventServiceTests;

public class GetOverlapEventInformation
{

    private readonly List<Event> _events;

    private readonly IOverlappingEventService _overlappingEventService;

    public GetOverlapEventInformation()
    {
        List<EventCollaborator> eventCollaborators1 = new EventCollaboratorListBuilder(1)
                                                      .WithOrganizer(new UserBuilder().WithId(1).Build(),
                                                                     new DateOnly(2024, 6, 2))
                                                      .WithParticipant(new UserBuilder().WithId(1).Build(),
                                                                       ConfirmationStatus.Accept,
                                                                       new DateOnly(2024, 6, 2),
                                                                       null)
                                                      .Build();

        Event event1 = new EventBuilder()
                       .WithId(1)
                       .WithTitle("1")
                       .WithLocation("1")
                       .WithDescription("1")
                       .WithDuration(new Duration(5, 6))
                       .WithEventCollaborators(eventCollaborators1)
                       .Build();

        List<EventCollaborator> eventCollaborators2 = new EventCollaboratorListBuilder(2)
                                                      .WithOrganizer(new UserBuilder().WithId(1).Build(),
                                                                     new DateOnly(2024, 6, 2))
                                                      .WithParticipant(new UserBuilder().WithId(1).Build(),
                                                                       ConfirmationStatus.Accept,
                                                                       new DateOnly(2024, 6, 2),
                                                                       null)
                                                      .Build();

        Event event2 = new EventBuilder()
                       .WithId(2)
                       .WithTitle("2")
                       .WithLocation("2")
                       .WithDescription("2")
                       .WithDuration(new Duration(6, 7))
                       .WithEventCollaborators(eventCollaborators2)
                       .Build();

        List<EventCollaborator> eventCollaborators3 = new EventCollaboratorListBuilder(3)
                                                      .WithOrganizer(new UserBuilder().WithId(1).Build(),
                                                                     new DateOnly(2024, 6, 2))
                                                      .Build();

        Event event3 = new EventBuilder()
                       .WithId(3)
                       .WithTitle("3")
                       .WithLocation("3")
                       .WithDescription("3")
                       .WithDuration(new Duration(6, 7))
                       .WithEventCollaborators(eventCollaborators3)
                       .Build();

        _events = [event1, event2, event3];

        _overlappingEventService = new OverlapEventService();
    }

    [Fact]
    public void Should_ReturnStringMessage_When_EventOverlap()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(2)
                                                      .WithOrganizer(new UserBuilder().WithId(1).Build(),
                                                                     new DateOnly(2024, 6, 2))
                                                      .Build();

        Event eventToCheckOverlap = new EventBuilder()
                                   .WithId(2)
                                   .WithTitle("2")
                                   .WithLocation("2")
                                   .WithDescription("2")
                                   .WithDuration(new Duration(5, 6))
                                   .WithEventCollaborators(eventCollaborators)
                                   .Build();

        StringBuilder expectedMessage = new($"2 overlaps with following events at {eventToCheckOverlap.Duration.GetDurationInFormat()} :-  ");

        expectedMessage.AppendLine($"Event Name : 1 , " +
                                   $"Date : {new DateOnly(2024, 6, 2)} , " +
                                   $"Duration : 5 AM - 6 AM");

        string? message = _overlappingEventService.GetOverlappedEventInformation(eventToCheckOverlap, _events);

        message.Should().NotBeNull();

        message.Should().Be(expectedMessage.ToString());
    }

    [Fact]
    public void Should_ReturnStringMessage_When_EventOverlapWithMultipleEvents()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(2)
                                                      .WithOrganizer(new UserBuilder().WithId(1).Build(),
                                                                     new DateOnly(2024, 6, 2))
                                                      .Build();

        Event eventToCheckOverlap = new EventBuilder()
                                   .WithId(4)
                                   .WithTitle("4")
                                   .WithLocation("4")
                                   .WithDescription("4")
                                   .WithDuration(new Duration(6, 7))
                                   .WithEventCollaborators(eventCollaborators)
                                   .Build();

        StringBuilder expectedMessage = new($"4 overlaps with following events at {eventToCheckOverlap.Duration.GetDurationInFormat()} :-  ");

        expectedMessage.AppendLine($"Event Name : 2 , " +
                                   $"Date : {new DateOnly(2024, 6, 2)} , " +
                                   $"Duration : 6 AM - 7 AM");

        expectedMessage.AppendLine($"Event Name : 3 , " +
                                   $"Date : {new DateOnly(2024, 6, 2)} , " +
                                   $"Duration : 6 AM - 7 AM");


        string? message = _overlappingEventService.GetOverlappedEventInformation(eventToCheckOverlap, _events);

        message.Should().NotBeNull();

        message.Should().Be(expectedMessage.ToString());
    }

    [Fact]
    public void Should_ReturnNull_When_NonOverlapEvent()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(2)
                                                      .WithOrganizer(new UserBuilder().WithId(1).Build(),
                                                                     new DateOnly(2024, 6, 3))
                                                      .Build();

        Event eventToCheckOverlap = new EventBuilder()
                                   .WithId(2)
                                   .WithTitle("2")
                                   .WithLocation("2")
                                   .WithDescription("2")
                                   .WithDuration(new Duration(5, 6))
                                   .WithEventCollaborators(eventCollaborators)
                                   .Build();

        string? message = _overlappingEventService.GetOverlappedEventInformation(eventToCheckOverlap, _events);

        message.Should().BeNull();
    }

    [Fact]
    public void Should_ReturnNull_When_EventToCheckOverlapIsNull()
    {
        string? message = _overlappingEventService.GetOverlappedEventInformation(null, _events);

        message.Should().BeNull();
    }

    [Fact]
    public void Should_ReturnNull_When_EventToCheckOverlapIsEmpty()
    {
        string? message = _overlappingEventService.GetOverlappedEventInformation(null, []);

        message.Should().BeNull();
    }
}
