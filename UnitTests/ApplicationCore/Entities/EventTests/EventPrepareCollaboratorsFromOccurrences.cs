using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventPrepareCollaboratorsFromOccurrences
{
    [Fact]
    public void Should_PrepareEventCollaboratorsListFromOccurrences_When_EventCollaboratorListIsNotEmpty()
    {
        List<EventCollaborator> expectedResult = new EventCollaboratorListBuilder(47)
                                                 .WithOrganizer(new UserBuilder(48).Build(),
                                                                new DateOnly(2024, 5, 31))
                                                 .WithParticipant(new UserBuilder(49).Build(),
                                                                  ConfirmationStatus.Pending,
                                                                  new DateOnly(2024, 5, 31),
                                                                  null)
                                                 .WithOrganizer(new UserBuilder(48).Build(),
                                                                new DateOnly(2024, 6, 1))
                                                 .WithParticipant(new UserBuilder(49).Build(),
                                                                  ConfirmationStatus.Pending,
                                                                  new DateOnly(2024, 6, 1),
                                                                  null)
                                                 .Build();

        List<DateOnly> occurrences = [new DateOnly(2024, 5, 31),
                                      new DateOnly(2024, 6, 1)];

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                    .WithOrganizer(new UserBuilder(48).Build(),
                                                                   new DateOnly())
                                                    .WithParticipant(new UserBuilder(49).Build(),
                                                                     ConfirmationStatus.Pending,
                                                                     new DateOnly(),
                                                                     null)
                                                    .Build();

        Event eventObj = new EventBuilder()
                        .WithEventCollaborators(eventCollaborators)
                        .Build();

        eventObj.PrepareCollaboratorsFromOccurrences(occurrences);

        eventObj.EventCollaborators.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void Should_BeEmptyList_When_EventCollaboratorListIsEmpty()
    {
        List<DateOnly> occurrences = [new DateOnly(2024, 5, 31),
                                      new DateOnly(2024, 6, 1)];

        Event eventObj = new()
        {
            EventCollaborators = []
        };

        eventObj.PrepareCollaboratorsFromOccurrences(occurrences);

        eventObj.EventCollaborators.Should().BeEmpty();
    }

    [Fact]
    public void Should_BeEmptyList_When_OccurrenceListIsEmpty()
    {
        List<DateOnly> occurrences = [];

        List<EventCollaborator> eventCollaborators = new EventCollaboratorListBuilder(47)
                                                    .WithOrganizer(new UserBuilder(48).Build(),
                                                                   new DateOnly())
                                                    .WithParticipant(new UserBuilder(49).Build(),
                                                                     ConfirmationStatus.Pending,
                                                                     new DateOnly(),
                                                                     null)
                                                    .Build();

        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(eventCollaborators)
                         .Build();

        eventObj.PrepareCollaboratorsFromOccurrences(occurrences);

        eventObj.EventCollaborators.Should().BeEmpty();
    }

    [Fact]
    public void Should_BeEmptyList_When_EventCollaboratorListIsNull()
    {
        List<DateOnly> occurrences = [];

        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(null)
                         .Build();

        eventObj.PrepareCollaboratorsFromOccurrences(occurrences);

        eventObj.EventCollaborators.Should().BeEmpty();
    }

    [Fact]
    public void Should_BeEmptyList_When_OccurrenceListIsNull()
    {
        List<DateOnly> occurrences = null;

        Event eventObj = new EventBuilder()
                         .WithEventCollaborators(null)
                         .Build();

        eventObj.PrepareCollaboratorsFromOccurrences(occurrences);

        eventObj.EventCollaborators.Should().BeEmpty();
    }
}
