using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventHasPendingResponseFromUser
{
    [Theory]
    [InlineData(50)]
    [InlineData(51)]
    public void Should_Return_False_When_UserIsNotPresentInEventCollaboratorList(int userId)
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                             .WithOrganizer(new UserBuilder(48).Build(), new DateOnly())
                                             .WithParticipant(new UserBuilder(49).Build(),
                                                              ConfirmationStatus.Proposed,
                                                              new DateOnly(),
                                                              null)
                                             .Build();

        Event eventObj = new EventBuilder()
                        .WithEventCollaborators(eventCollaborators)
                        .Build();

        bool result = eventObj.HasPendingResponseFromUser(userId);

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(48)]
    [InlineData(49)]
    public void Should_Return_False_When_NoUserWithPendingResponsePresentInEventCollaboratorList(int userId)
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                             .WithOrganizer(new UserBuilder(48).Build(), new DateOnly())
                                             .WithParticipant(new UserBuilder(49).Build(),
                                                              ConfirmationStatus.Proposed,
                                                              new DateOnly(),
                                                              null)
                                             .WithParticipant(new UserBuilder(49).Build(),
                                                              ConfirmationStatus.Accept,
                                                              new DateOnly(),
                                                              null)
                                             .Build();

        Event eventObj = new EventBuilder()
                 .WithEventCollaborators(eventCollaborators)
                 .Build();

        bool result = eventObj.HasPendingResponseFromUser(userId);

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(50)]
    public void Should_Return_True_When_UserWithPendingResponsePresentInEventCollaboratorList(int userId)
    {

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                             .WithOrganizer(new UserBuilder(48).Build(), new DateOnly())
                                             .WithParticipant(new UserBuilder(49).Build(),
                                                              ConfirmationStatus.Proposed,
                                                              new DateOnly(),
                                                              null)
                                             .WithParticipant(new UserBuilder(50).Build(),
                                                              ConfirmationStatus.Pending,
                                                              new DateOnly(),
                                                              null)
                                             .Build();

        Event eventObj = new EventBuilder()
                        .WithEventCollaborators(eventCollaborators)
                        .Build();

        bool result = eventObj.HasPendingResponseFromUser(userId);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(50)]
    [InlineData(51)]
    public void Should_Return_True_When_MultipleUserWithPendingResponsePresentInEventCollaboratorList(int userId)
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                             .WithOrganizer(new UserBuilder(48).Build(), new DateOnly())
                                             .WithParticipant(new UserBuilder(49).Build(),
                                                              ConfirmationStatus.Proposed,
                                                              new DateOnly(),
                                                              null)
                                             .WithParticipant(new UserBuilder(50).Build(),
                                                              ConfirmationStatus.Pending,
                                                              new DateOnly(),
                                                              null)
                                             .WithParticipant(new UserBuilder(51).Build(),
                                                              ConfirmationStatus.Pending,
                                                              new DateOnly(),
                                                              null)
                                             .Build();

        Event eventObj = new EventBuilder()
                        .WithEventCollaborators(eventCollaborators)
                        .Build();

        bool result = eventObj.HasPendingResponseFromUser(userId);

        result.Should().BeTrue();
    }

}
