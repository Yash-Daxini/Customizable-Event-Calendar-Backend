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

public class AddEventCollaborator : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public AddEventCollaborator(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_Return_AddedEventCollaboratorId_When_EventCollaboratorIsValid()
    {
        //Arrange
        _dbContext = await new EventCollaboratorRepositoryDBContext().GetDatabaseContext();

        UserDataModel userDataModel = new UserDataModelBuilder()
                                      .WithId(1)
                                      .WithUserName("a")
                                      .WithEmail("a@gmail.com")
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
                                        .Build();

        await new DatabaseBuilder(_dbContext)
             .WithUser(userDataModel)
             .WithEvent(eventDataModel)
             .Build();


        User user = new UserBuilder(1)
                    .WithName("a")
                    .WithEmail("a")
                    .Build();

        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Organizer)
                                              .WithConfirmationStatus(ConfirmationStatus.Accept)
                                              .WithEventDate(new DateOnly())
                                              .WithProposedDuration(null)
                                              .WithUser(user)
                                              .WithEventId(1)
                                              .Build();

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

        //Act
        int id = await eventCollaboratorRepository.Add(eventCollaborator);

        bool isContains = _dbContext.EventCollaborators.Any(e => e.Id == id);

        //Assert
        id.Should().BeGreaterThan(0);
        isContains.Should().BeTrue();
    }
}
