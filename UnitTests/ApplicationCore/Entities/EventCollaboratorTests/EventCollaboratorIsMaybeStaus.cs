using Core.Entities.Enums;
using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsMaybeStaus
{
    [Fact]
    public void Should_ReturnsTrue_When_EventCollaboratorWithMaybeStatus()
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = ConfirmationStatus.Maybe,
        };

        bool result = eventCollaborator.IsMaybeStatus();

        Assert.True(result);
    }

    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Accept)]
    [InlineData(ConfirmationStatus.Pending)]
    [InlineData(ConfirmationStatus.Proposed)]
    public void Should_ReturnsFalse_When_EventCollaboratorNotWithMaybeStatus(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = confirmationStatus,
        };

        bool result = eventCollaborator.IsMaybeStatus();

        Assert.False(result);
    }
}
