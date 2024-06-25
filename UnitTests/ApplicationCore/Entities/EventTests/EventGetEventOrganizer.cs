using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventGetEventOrganizer
{
    [Fact]
    public void Should_ReturnOrganizerOfEvent_When_MultipleEventCollaboratorsPresent()
    {
        User user1 =  new UserBuilder(48)
                     .WithName("a")
                     .WithEmail("a@gmail.com")
                     .WithPassword("a")
                     .Build();

        User user2 = new UserBuilder(49)
                     .WithName("b")
                     .WithEmail("b@gmail.com")
                     .WithPassword("b")
                     .Build();

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                      .WithOrganizer(user1,new DateOnly())
                                                      .WithParticipant(user2,
                                                                       ConfirmationStatus.Accept,
                                                                       new DateOnly(),
                                                                       null)
                                                      .Build();

        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        User? actualUser = eventObj.GetEventOrganizer();

        actualUser.Should().BeEquivalentTo(user1);
    }

    [Fact]
    public void Should_ReturnOrganizerOfEvent_When_OnlyOrganizerPresent()
    {
        User user1 = new UserBuilder(48)
                     .WithName("a")
                     .WithEmail("a@gmail.com")
                     .WithPassword("a")
                     .Build();

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                      .WithOrganizer(user1, new DateOnly())
                                                      .Build();

        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        User? actualUser = eventObj.GetEventOrganizer();

        actualUser.Should().BeEquivalentTo(user1);
    }

    [Fact]
    public void Should_ReturnOrganizerOfEventAsNull_When_EventCollaboratorsIsNull()
    {
        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(null)
                         .Build();

        User? actualResult = eventObj.GetEventOrganizer();

        actualResult.Should().BeNull();
    }

    [Fact]
    public void Should_ReturnOrganizerOfEventAsNull_When_EventCollaboratorsIsEmpty()
    {
        Event eventObj = new EventBuilder()
                         .WithEventCollaborators([])
                         .Build();

        User? actualResult = eventObj.GetEventOrganizer();

        actualResult.Should().BeNull();
    }
}