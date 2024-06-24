using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventGetOverlapDate
{
    private readonly Event _event;
    private readonly User _user1;
    private readonly User _user2;

    public EventGetOverlapDate()
    {
        _user1 = new UserBuilder()
             .WithId(48)
             .WithName("a")
             .WithEmail("a@gmail.com")
             .WithPassword("a")
             .Build();

        _user2 = new UserBuilder()
             .WithId(49)
             .WithName("b")
             .WithEmail("b@gmail.com")
             .WithPassword("b")
             .Build();

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(_user1, new DateOnly(2024, 5, 31))
                                                     .WithParticipant(_user2,
                                                                      ConfirmationStatus.Accept,
                                                                      new DateOnly(2024, 5, 31),
                                                                      null)
                                                     .Build();

        _event = new EventBuilder()
                 .WithEventCollaborators(eventCollaborators)
                 .Build();
    }

    [Fact]
    public void Should_ReturnsNull_When_OverlapNotOccur()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                     .WithOrganizer(_user1, new DateOnly(2024, 6, 1))
                                                     .WithParticipant(_user2,
                                                                      ConfirmationStatus.Accept,
                                                                      new DateOnly(2024, 6, 1),
                                                                      null)
                                                     .Build();

        Event eventToCheckOverlap = new EventBuilder()
                                    .WithEventCollaborators(eventCollaborators)
                                    .Build();

        DateOnly? overlapDate = _event.GetOverlapDate(eventToCheckOverlap);

        overlapDate.Should().BeNull();
    }

    [Fact]
    public void Should_ReturnsOverlapDate_When_OverlapOccur()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                             .WithOrganizer(_user1, new DateOnly(2024, 5, 31))
                                             .WithParticipant(_user2,
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(2024, 5, 31),
                                                              null)
                                             .WithOrganizer(_user1, new DateOnly(2024, 6, 1))
                                             .WithParticipant(_user2,
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(2024, 6, 1),
                                                              null)
                                             .Build();

        Event eventToCheckOverlap = new EventBuilder()
                                    .WithEventCollaborators(eventCollaborators)
                                    .Build();

        DateOnly? overlapDate = _event.GetOverlapDate(eventToCheckOverlap);

        overlapDate.Should().Be(new(2024, 5, 31));
    }

    [Fact]
    public void Should_ReturnsNull_When_PassedEventIsNull()
    {
        DateOnly? overlapDate = _event.GetOverlapDate(null);

        overlapDate.Should().BeNull();
    }
}