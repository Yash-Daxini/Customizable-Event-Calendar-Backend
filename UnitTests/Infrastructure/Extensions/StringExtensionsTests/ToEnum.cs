using Core.Entities.Enums;
using FluentAssertions;
using Infrastructure.Extensions;

namespace UnitTests.Infrastructure.Extensions.StringExtensionsTests;

public class ToEnum
{
    [Theory]
    [InlineData("Accept", ConfirmationStatus.Accept)]
    [InlineData("Reject", ConfirmationStatus.Reject)]
    [InlineData("Maybe", ConfirmationStatus.Maybe)]
    [InlineData("Pending", ConfirmationStatus.Pending)]
    [InlineData("Proposed", ConfirmationStatus.Proposed)]
    public void Should_Return_ConfirmationStatusEnumValue_When_GivenValidString(string confirmationString, ConfirmationStatus expectedResult)
    {
        var actualResult = confirmationString.ToEnum<ConfirmationStatus>();

        actualResult.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("accept", ConfirmationStatus.Accept)]
    [InlineData("reject", ConfirmationStatus.Reject)]
    [InlineData("maybe", ConfirmationStatus.Maybe)]
    [InlineData("pending", ConfirmationStatus.Pending)]
    [InlineData("proposed", ConfirmationStatus.Proposed)]
    public void Should_Return_ArgumentException_When_GivenInValidStringOfConfirmationStatus(string confirmationString, ConfirmationStatus expectedResult)
    {
        Action action = () => confirmationString.ToEnum<ConfirmationStatus>();

        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("Organizer", EventCollaboratorRole.Organizer)]
    [InlineData("Participant", EventCollaboratorRole.Participant)]
    [InlineData("Collaborator", EventCollaboratorRole.Collaborator)]
    public void Should_Return_EventCollaboratorRoleEnumValue_When_GivenValidString(string confirmationString, EventCollaboratorRole expectedResult)
    {
        var actualResult = confirmationString.ToEnum<EventCollaboratorRole>();

        actualResult.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("organizer", EventCollaboratorRole.Organizer)]
    [InlineData("participant", EventCollaboratorRole.Participant)]
    [InlineData("collaborator", EventCollaboratorRole.Collaborator)]
    public void Should_Return_ArgumentException_When_GivenInValidStringOfEventCollaboratorRole(string confirmationString, EventCollaboratorRole expectedResult)
    {
        Action action = () => confirmationString.ToEnum<EventCollaboratorRole>();

        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("None", Frequency.None)]
    [InlineData("Daily", Frequency.Daily)]
    [InlineData("Weekly", Frequency.Weekly)]
    [InlineData("Monthly", Frequency.Monthly)]
    [InlineData("Yearly", Frequency.Yearly)]
    public void Should_Return_FrequencyEnumValue_When_GivenValidString(string confirmationString, Frequency expectedResult)
    {
        var actualResult = confirmationString.ToEnum<Frequency>();

        actualResult.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("none", Frequency.None)]
    [InlineData("daily", Frequency.Daily)]
    [InlineData("weekly", Frequency.Weekly)]
    [InlineData("monthly", Frequency.Monthly)]
    [InlineData("yearly", Frequency.Yearly)]
    public void Should_Return_ArgumentException_When_GivenInValidStringOfFrequency(string confirmationString, Frequency expectedResult)
    {
        Action action = () => confirmationString.ToEnum<Frequency>();

        action.Should().Throw<ArgumentException>();
    }
}
