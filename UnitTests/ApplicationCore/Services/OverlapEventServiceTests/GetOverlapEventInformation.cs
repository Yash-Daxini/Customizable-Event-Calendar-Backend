using System.Text;
using Core.Entities;
using Core.Entities.Enums;
using Core.Interfaces.IServices;
using Core.Services;
using FluentAssertions;
using UnitTests.Builders.EntityBuilder;
using Core.Exceptions;

namespace UnitTests.ApplicationCore.Services.OverlapEventServiceTests;

public class GetOverlapEventInformation
{

    private readonly List<Event> _events;

    private readonly IOverlappingEventService _overlappingEventService;

    public GetOverlapEventInformation()
    {
        List<EventCollaborator> eventCollaborators1 = new EventCollaboratorListBuilder(1)
                                                      .WithOrganizer(new UserBuilder(1).Build(),
                                                                     new DateOnly(2024, 6, 2))
                                                      .WithParticipant(new UserBuilder(1).Build(),
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
                                                      .WithOrganizer(new UserBuilder(1).Build(),
                                                                     new DateOnly(2024, 6, 2))
                                                      .WithParticipant(new UserBuilder(1).Build(),
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
                                                      .WithOrganizer(new UserBuilder(1).Build(),
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
    public void Should_Throw_EventOverlapException_When_EventOverlapWithSingleEvent()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(2)
                                                      .WithOrganizer(new UserBuilder(1).Build(),
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

        StringBuilder expectedMessage = new($" 2 overlaps with following events at {eventToCheckOverlap.Duration.GetDurationInFormat()} :-  ");

        expectedMessage.AppendLine($"Event Name : 1 , " +
                                   $"Date : {new DateOnly(2024, 6, 2)} , " +
                                   $"Duration : 5 AM - 6 AM");

        Action action = () => _overlappingEventService.CheckOverlap(eventToCheckOverlap, _events);

        action.Should().Throw<EventOverlapException>()
              .WithMessage(expectedMessage.ToString());
    }

    [Fact]
    public void Should_Throw_EventOverlapException_When_EventOverlapWithMultipleEvents()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(2)
                                                      .WithOrganizer(new UserBuilder(1).Build(),
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

        StringBuilder expectedMessage = new($" 4 overlaps with following events at {eventToCheckOverlap.Duration.GetDurationInFormat()} :-  ");

        expectedMessage.AppendLine($"Event Name : 2 , " +
                                   $"Date : {new DateOnly(2024, 6, 2)} , " +
                                   $"Duration : 6 AM - 7 AM");

        expectedMessage.AppendLine($"Event Name : 3 , " +
                                   $"Date : {new DateOnly(2024, 6, 2)} , " +
                                   $"Duration : 6 AM - 7 AM");


        Action action = () => _overlappingEventService.CheckOverlap(eventToCheckOverlap, _events);

        action.Should().Throw<EventOverlapException>()
              .WithMessage(expectedMessage.ToString());
    }

    [Fact]
    public void Should_NotThrow_Exception_When_EventNotOverlap()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(2)
                                                      .WithOrganizer(new UserBuilder(1).Build(),
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

        Action action = () => _overlappingEventService.CheckOverlap(eventToCheckOverlap, _events);

        action.Should().NotThrow<EventOverlapException>();
    }

    [Fact]
    public void Should_NotThrow_Exception_When_EventToCheckOverlapIsNull()
    {
        Action action = () => _overlappingEventService.CheckOverlap(null, _events);

        action.Should().NotThrow<EventOverlapException>();
    }

    [Fact]
    public void Should_NotThrow_Exception_When_EventListIsEmpty()
    {
        Action action = () => _overlappingEventService.CheckOverlap(null, []);

        action.Should().NotThrow<EventOverlapException>();
    }
}
