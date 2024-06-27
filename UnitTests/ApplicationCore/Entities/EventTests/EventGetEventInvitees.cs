using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventGetEventInvitees
{

    [Fact]
    public void Should_Return_EmptyList_When_EventCollaboratorsIsNull()
    {
        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(null)
                         .Build();

        List<EventCollaborator> actualResult = eventObj.GetEventInvitees();

        actualResult.Should().BeEmpty();
    }

    [Fact]
    public void Should_Return_EmptyList_When_EventCollaboratorsIsEmpty()
    {
        Event eventObj = new EventBuilder()
                         .WithEventCollaborators([])
                         .Build();

        List<EventCollaborator> actualResult = eventObj.GetEventInvitees();

        actualResult.Should().BeEmpty();
    }

    [Fact]
    public void Should_Return_ListOfEventCollaborators_When_EventCollaboratorsIsNotNullAndNotEmpty()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                      .WithOrganizer(new UserBuilder(48).Build(),
                                                                     new DateOnly())
                                                      .WithParticipant(new UserBuilder(49).Build(),
                                                                       ConfirmationStatus.Accept,
                                                                       new DateOnly(),
                                                                       null)
                                                      .Build();       

        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        List<EventCollaborator> expectedResult = new EventCollaboratorListBuilder(47)
                                                 .WithParticipant(new UserBuilder(49).Build(),
                                                                       ConfirmationStatus.Accept,
                                                                       new DateOnly(),
                                                                       null)
                                                 .Build();


        List<EventCollaborator> actualResult = eventObj.GetEventInvitees();

        actualResult.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void Should_ReturnListOfEventCollaborators_When_EventCollaboratorsContainsCollaborator()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                      .WithOrganizer(new UserBuilder(48).Build(),
                                                                     new DateOnly())
                                                      .WithParticipant(new UserBuilder(49).Build(),
                                                                       ConfirmationStatus.Accept,
                                                                       new DateOnly(),
                                                                       null)
                                                      .WithCollaborator(new UserBuilder(50).Build(),new DateOnly())
                                                      .Build();

        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        List<EventCollaborator> expectedResult = new EventCollaboratorListBuilder(47)
                                                 .WithParticipant(new UserBuilder(49).Build(),
                                                                       ConfirmationStatus.Accept,
                                                                       new DateOnly(),
                                                                       null)
                                                 .Build();

        List<EventCollaborator> actualResult = eventObj.GetEventInvitees();

        actualResult.Should().BeEquivalentTo(expectedResult);
    }
}
