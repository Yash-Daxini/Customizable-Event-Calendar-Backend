using Core.Entities.Enums;
using Core.Entities;
using UnitTests.Builders.EntityBuilder;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsStatusProposed
{
    [Fact]
    public void Should_Returns_True_When_EventCollaboratorWithProposedStatus()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(ConfirmationStatus.Proposed)
                                              .Build();

        bool result = eventCollaborator.IsStatusProposed();

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Accept)]
    [InlineData(ConfirmationStatus.Pending)]
    [InlineData(ConfirmationStatus.Maybe)]
    public void Should_Returns_False_When_EventCollaboratorNotWithProposedStatus(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(confirmationStatus)
                                              .Build();

        bool result = eventCollaborator.IsStatusProposed();

        result.Should().BeFalse();
    }
}
