using Core.Entities.Enums;
using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsAcceptStatus
{
    [Fact]
    public void IsAcceptStatusReturnTrueIfAcceptStatus()
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = ConfirmationStatus.Accept,
        };

        bool result = eventCollaborator.IsAcceptStatus();

        Assert.True(result);
    }

    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Proposed)]
    [InlineData(ConfirmationStatus.Pending)]
    [InlineData(ConfirmationStatus.Maybe)]
    public void IsAcceptStatusReturnFalseIfNotAcceptStatus(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = confirmationStatus,
        };

        bool result = eventCollaborator.IsAcceptStatus();

        Assert.False(result);
    }
}
