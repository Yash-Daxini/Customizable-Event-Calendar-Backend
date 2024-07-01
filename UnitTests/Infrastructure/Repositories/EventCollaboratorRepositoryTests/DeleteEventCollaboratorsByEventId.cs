using AutoMapper;
using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Repositories;
using UnitTests.Builders.DataModelBuilder;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.Infrastructure.Repositories.EventCollaboratorRepositoryTests;

public class DeleteEventCollaboratorsByEventId : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;
    public DeleteEventCollaboratorsByEventId(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_DeleteEventCollaborators_When_EventCollaboratorsHasGivenEventId()
    {
        UserDataModel userDataModel1 = new UserDataModelBuilder()
                                      .WithId(1)
                                      .WithUserName("a")
                                      .WithEmail("a@gmail.com")
                                      .Build();

        UserDataModel userDataModel2 = new UserDataModelBuilder()
                                      .WithId(2)
                                      .WithUserName("b")
                                      .WithEmail("b@gmail.com")
                                      .Build();


        List<EventCollaboratorDataModel> eventCollaborators = new EventCollaboratorDataModelListBuilder(1)
                                                              .WithOrganizer(1, new DateOnly(2024, 6, 7))
                                                              .WithParticipant(2, "Accept", new DateOnly(2024, 6, 7), null, null)
                                                              .Build();

        EventDataModel eventDataModel = new EventDataModelBuilder()
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
                                        .WithEventCollaborators(eventCollaborators)
                                        .Build();

        _dbContext = new DatabaseBuilder()
             .WithUser(userDataModel1)
             .WithUser(userDataModel2)
             .WithEvent(eventDataModel)
             .Build();

        User user = new UserBuilder(1)
                    .WithName("a")
                    .WithEmail("a")
                    .Build();

        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithId(1)
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Organizer)
                                              .WithConfirmationStatus(ConfirmationStatus.Accept)
                                              .WithEventDate(new DateOnly())
                                              .WithProposedDuration(null)
                                              .WithUser(user)
                                              .WithEventId(1)
                                              .Build();

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

        await eventCollaboratorRepository.DeleteEventCollaboratorsByEventId(1);

        bool isContains = _dbContext.EventCollaborators.Any(e => e.EventId == 1);

        isContains.Should().BeFalse();
    }
}
