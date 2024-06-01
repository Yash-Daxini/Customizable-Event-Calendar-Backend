using Core.Entities.Enums;
using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsMaybeStaus
{
    [Fact]
    public void IsMaybeStatusReturnTrueIfMaybeStatus()
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
    public void IsMaybeStatusReturnFalseIfNotMaybeStatus(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = confirmationStatus,
        };

        bool result = eventCollaborator.IsMaybeStatus();

        Assert.False(result);
    }
}
