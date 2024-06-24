﻿using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventGetEventInvitees
{

    [Fact]
    public void Should_ReturnEmptyList_When_EventCollaboratorsIsNull()
    {
        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(null)
                         .Build();

        List<EventCollaborator> actualResult = eventObj.GetEventInvitees();

        actualResult.Should().BeEmpty();
    }

    [Fact]
    public void Should_ReturnEmptyList_When_EventCollaboratorsIsEmpty()
    {
        Event eventObj = new EventBuilder()
                         .WithEventCollaborators([])
                         .Build();

        List<EventCollaborator> actualResult = eventObj.GetEventInvitees();

        actualResult.Should().BeEmpty();
    }

    [Fact]
    public void Should_ReturnListOfEventCollaborators_When_EventCollaboratorsAvailable()
    {
        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                      .WithOrganizer(new UserBuilder().WithId(48).Build(),
                                                                     new DateOnly())
                                                      .WithParticipant(new UserBuilder().WithId(49).Build(),
                                                                       ConfirmationStatus.Accept,
                                                                       new DateOnly(),
                                                                       null)
                                                      .Build();       

        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        List<EventCollaborator> expectedResult = new EventCollaboratorListBuilder(47)
                                                 .WithParticipant(new UserBuilder().WithId(49).Build(),
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
                                                      .WithOrganizer(new UserBuilder().WithId(48).Build(),
                                                                     new DateOnly())
                                                      .WithParticipant(new UserBuilder().WithId(49).Build(),
                                                                       ConfirmationStatus.Accept,
                                                                       new DateOnly(),
                                                                       null)
                                                      .WithCollaborator(new UserBuilder().WithId(50).Build(),new DateOnly())
                                                      .Build();

        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        List<EventCollaborator> expectedResult = new EventCollaboratorListBuilder(47)
                                                 .WithParticipant(new UserBuilder().WithId(49).Build(),
                                                                       ConfirmationStatus.Accept,
                                                                       new DateOnly(),
                                                                       null)
                                                 .Build();

        List<EventCollaborator> actualResult = eventObj.GetEventInvitees();

        actualResult.Should().BeEquivalentTo(expectedResult);
    }
}
