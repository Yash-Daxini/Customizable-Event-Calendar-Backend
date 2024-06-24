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
    public void Should_ReturnsFalse_When_UserIsNotAvailableAsEventCollaborator(int userId)
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                             .WithOrganizer(new UserBuilder().WithId(48).Build(), new DateOnly())
                                             .WithParticipant(new UserBuilder().WithId(49).Build(),
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
    public void Should_ReturnsFalse_When_UserHasNotPendingResponse(int userId)
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                             .WithOrganizer(new UserBuilder().WithId(48).Build(), new DateOnly())
                                             .WithParticipant(new UserBuilder().WithId(49).Build(),
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
    [InlineData(50)]
    public void Should_ReturnsTrue_When_UserHasPendingResponse(int userId)
    {

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                             .WithOrganizer(new UserBuilder().WithId(48).Build(), new DateOnly())
                                             .WithParticipant(new UserBuilder().WithId(49).Build(),
                                                              ConfirmationStatus.Proposed,
                                                              new DateOnly(),
                                                              null)
                                             .WithParticipant(new UserBuilder().WithId(50).Build(),
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
    public void Should_ReturnsTrue_When_MultipleUserHasPendingResponse(int userId)
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                             .WithOrganizer(new UserBuilder().WithId(48).Build(), new DateOnly())
                                             .WithParticipant(new UserBuilder().WithId(49).Build(),
                                                              ConfirmationStatus.Proposed,
                                                              new DateOnly(),
                                                              null)
                                             .WithParticipant(new UserBuilder().WithId(50).Build(),
                                                              ConfirmationStatus.Pending,
                                                              new DateOnly(),
                                                              null)
                                             .WithParticipant(new UserBuilder().WithId(51).Build(),
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
