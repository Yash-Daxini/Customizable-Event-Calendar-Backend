using Core.Entities;
using Core.Entities.Enums;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorAcceptConfirmationStatus
{
    [Fact]
    public void AcceptConfirmationStatusIfAlreadyAccepted()
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = ConfirmationStatus.Accept
        };

        eventCollaborator.AcceptConfirmationStatus();

        bool result = eventCollaborator.IsAcceptStatus();

        Assert.True(result);
    }
    
    
    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Proposed)]
    [InlineData(ConfirmationStatus.Maybe)]
    [InlineData(ConfirmationStatus.Pending)]
    public void AcceptConfirmationStatusIfNotAccepted(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = confirmationStatus
        };

        eventCollaborator.AcceptConfirmationStatus();

        bool result = eventCollaborator.IsAcceptStatus();

        Assert.True(result);
    }
}
