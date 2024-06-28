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

public class GetEventCollaboratorById : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public GetEventCollaboratorById(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_Return_EventCollaborators_When_EventCollaboratorAvailableWithGivenId()
    {
        _dbContext = await new EventCollaboratorRepositoryDBContext().GetDatabaseContext();

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

        EventCollaborator expectedResult = new EventCollaboratorBuilder()
                                              .WithId(1)
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Organizer)
                                              .WithConfirmationStatus(ConfirmationStatus.Accept)
                                              .WithEventDate(new DateOnly(2024,6,7))
                                              .WithProposedDuration(null)
                                              .WithUser(user)
                                              .WithEventId(1)
                                              .Build();

        EventCollaborator? eventCollaboratorById =  await eventCollaboratorRepository.GetEventCollaboratorById(1);

        eventCollaboratorById.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public async Task Should_Return_Null_When_EventCollaboratorNotAvailableWithGivenId()
    {
        _dbContext = await new EventCollaboratorRepositoryDBContext().GetDatabaseContext();

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

        EventCollaborator? eventCollaborator =  await eventCollaboratorRepository.GetEventCollaboratorById(5);

        eventCollaborator.Should().BeNull();
    }
}
