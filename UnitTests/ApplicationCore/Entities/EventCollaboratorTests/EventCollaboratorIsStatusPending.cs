using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsStatusPending
{
    [Fact]
    public void Should_Returns_True_When_EventCollaboratorWithPendingStatus()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(ConfirmationStatus.Pending)
                                              .Build();

        bool result = eventCollaborator.IsStatusPending();

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Accept)]
    [InlineData(ConfirmationStatus.Proposed)]
    [InlineData(ConfirmationStatus.Maybe)]
    public void Should_Returns_False_When_EventCollaboratorNotWithPendingStatus(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(confirmationStatus)
                                              .Build();

        bool result = eventCollaborator.IsStatusPending();

        result.Should().BeFalse();
    }
}
