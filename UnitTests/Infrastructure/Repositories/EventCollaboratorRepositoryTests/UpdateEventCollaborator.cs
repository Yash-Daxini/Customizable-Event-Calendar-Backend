using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using FluentAssertions;
using Core.Entities.Enums;
using UnitTests.Builders.EntityBuilder;
using Infrastructure.DataModels;
using UnitTests.Builders.DataModelBuilder;

namespace UnitTests.Infrastructure.Repositories.EventCollaboratorRepositoryTests;

public class UpdateEventCollaborator : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public UpdateEventCollaborator(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_UpdateEventCollaborator_When_EventCollaboratorAvailableWithGivenId()
    {
        UserDataModel userDataModel = new UserDataModelBuilder()
                              .WithId(1)
                              .WithUserName("a")
                              .WithEmail("a@gmail.com")
                              .Build();

        List<EventCollaboratorDataModel> eventCollaborators = new EventCollaboratorDataModelListBuilder(1)
                                                              .WithOrganizer(1, new DateOnly(2024, 6, 7))
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
             .WithUser(userDataModel)
             .WithEvent(eventDataModel)
             .Build();

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

        User user = new UserBuilder(1)
                    .WithName("a")
                    .WithEmail("a@gmail.com")
                    .Build();

        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithId(1)
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Organizer)
                                              .WithConfirmationStatus(ConfirmationStatus.Pending) // Status changed
                                              .WithEventDate(new DateOnly()) //Date changed
                                              .WithProposedDuration(null)
                                              .WithUser(user)
                                              .WithEventId(1)
                                              .Build();

        await eventCollaboratorRepository.Update(eventCollaborator);

        EventCollaborator? updatedEventCollaborator = await eventCollaboratorRepository.GetEventCollaboratorById(1);

        updatedEventCollaborator.Should().BeEquivalentTo(eventCollaborator);
    }
}
