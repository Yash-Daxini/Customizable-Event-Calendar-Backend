﻿using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventGetEventOrganizer
{
    [Fact]
    public void Should_Return_OrganizerOfEvent_When_MultipleEventCollaboratorsPresentInList()
    {
        User user1 = new UserBuilder(48)
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
                                                      .WithOrganizer(user1, new DateOnly())
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
    public void Should_Return_OrganizerOfEvent_When_OnlyOrganizerPresentInEventCollaboratorList()
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
    public void Should_Return_Null_When_OrganizerNotPresentInEventCollaboratorList()
    {
        User user1 = new UserBuilder(48)
                     .WithName("a")
                     .WithEmail("a@gmail.com")
                     .WithPassword("a")
                     .Build();

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                      .WithParticipant(user1,
                                                                       ConfirmationStatus.Accept,
                                                                       new DateOnly(),
                                                                       null)
                                                      .Build();

        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        User? actualUser = eventObj.GetEventOrganizer();

        actualUser.Should().BeNull();
    }

    [Fact]
    public void Should_Return_Null_When_EventCollaboratorListIsNull()
    {
        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(null)
                         .Build();

        User? actualResult = eventObj.GetEventOrganizer();

        actualResult.Should().BeNull();
    }

    [Fact]
    public void Should_Return_Null_When_EventCollaboratorListIsEmpty()
    {
        Event eventObj = new EventBuilder()
                         .WithEventCollaborators([])
                         .Build();

        User? actualResult = eventObj.GetEventOrganizer();

        actualResult.Should().BeNull();
    }
}