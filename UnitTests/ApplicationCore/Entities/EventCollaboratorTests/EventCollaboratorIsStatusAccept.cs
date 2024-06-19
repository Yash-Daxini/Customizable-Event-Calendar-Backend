using Core.Entities.Enums;
using Core.Entities;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsStatusAccept
{
    [Fact]
    public void Should_ReturnTrue_When_StatusIsAccept()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(ConfirmationStatus.Accept)
                                              .Build();

        bool result = eventCollaborator.IsStatusAccept();

        Assert.True(result);
    }

    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Proposed)]
    [InlineData(ConfirmationStatus.Pending)]
    [InlineData(ConfirmationStatus.Maybe)]
    public void Should_ReturnFalse_When_StatusIsNotAccept(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(confirmationStatus)
                                              .Build();

        bool result = eventCollaborator.IsStatusAccept();

        Assert.False(result);
    }
}
