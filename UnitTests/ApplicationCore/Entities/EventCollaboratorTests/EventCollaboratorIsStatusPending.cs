using Core.Entities;
using Core.Entities.Enums;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsStatusPending
{
    [Fact]
    public void Should_ReturnsTrue_When_EventCollaboratorWithPendingStatus()
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = ConfirmationStatus.Pending,
        };

        bool result = eventCollaborator.IsStatusPending();

        Assert.True(result);
    }

    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Accept)]
    [InlineData(ConfirmationStatus.Proposed)]
    [InlineData(ConfirmationStatus.Maybe)]
    public void Should_ReturnsFalse_When_EventCollaboratorNotWithPendingStatus(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = confirmationStatus,
        };

        bool result = eventCollaborator.IsStatusPending();

        Assert.False(result);
    }
}
