﻿using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders.EntityBuilder;
using Core.Entities.Enums;
using Infrastructure.DataModels;
using UnitTests.Builders.DataModelBuilder;

namespace UnitTests.Infrastructure.Repositories.EventRepositoryTests;

public class UpdateEvent : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContextEvent;
    private readonly IMapper _mapper;

    public UpdateEvent(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_UpdateEvent_When_EventAvailableWithId()
    {
        _dbContextEvent = await new EventRepositoryDBContext().GetDatabaseContext();

        UserDataModel userDataModel1 = new UserDataModelBuilder()
              .WithId(1)
              .WithUserName("a")
              .WithEmail("a@gmail.com")
              .Build();

        List<EventCollaboratorDataModel> eventCollaboratorDataModels1 = new EventCollaboratorDataModelListBuilder(1)
                                                               .WithOrganizer(1, new DateOnly(2024, 6, 7))
                                                               .Build();

        EventDataModel eventDataModel1 = new EventDataModelBuilder()
                                        .WithTitle("Test")
                                        .WithDescription("Test")
                                        .WithLocation("Test")
                                        .WithUserId(1)
                                        .WithStartHour(1)
                                        .WithEndHour(2)
                                        .WithStartDate(new DateOnly(2024, 6, 7))
                                        .WithEndDate(new DateOnly(2024, 6, 7))
                                        .WithFrequency("None")
                                        .WithInterval(1)
                                        .WithByMonth(null)
                                        .WithByMonthDay(null)
                                        .WithWeekOrder(null)
                                        .WithEventCollaborators(eventCollaboratorDataModels1)
                                        .Build();

        await new DatabaseBuilder(_dbContextEvent)
              .WithUser(userDataModel1)
              .WithEvent(eventDataModel1)
              .Build();

        EventRepository eventRepository = new(_dbContextEvent, _mapper);

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContextEvent, _mapper);

        _dbContextEvent.ChangeTracker.Clear();

        User user1 = new UserBuilder(1)
                     .WithName("a")
                     .WithEmail("a@gmail.com")
                     .Build();

        SingleInstanceRecurrencePattern singleInstanceRecurrencePattern = new SingleInstanceRecurrencePatternBuilder()
                                                                          .WithStartDate(new DateOnly(2024, 6, 7))
                                                                          .WithEndDate(new DateOnly(2024, 6, 7))
                                                                          .WithInterval(1)
                                                                          .Build();

        List<EventCollaborator> eventCollaborators1 = new EventCollaboratorListBuilder(1)
                                                      .WithOrganizer(user1, new DateOnly(2024, 6, 7))
                                                      .Build();


        Event eventToUpdate = new EventBuilder()
                              .WithId(1)
                              .WithTitle("Test")
                              .WithDescription("Test")
                              .WithLocation("Test")
                              .WithDuration(new Duration(1, 2))
                              .WithRecurrencePattern(singleInstanceRecurrencePattern)
                              .WithEventCollaborators(eventCollaborators1)
                              .Build();


        await eventCollaboratorRepository.DeleteEventCollaboratorsByEventId(1);

        await eventRepository.Update(eventToUpdate);

        Event? updatedEvent = await eventRepository.GetEventById(1);

        updatedEvent.Should().BeEquivalentTo(eventToUpdate, option => option.For(e => e.EventCollaborators).Exclude(e => e.Id));
    }
}
