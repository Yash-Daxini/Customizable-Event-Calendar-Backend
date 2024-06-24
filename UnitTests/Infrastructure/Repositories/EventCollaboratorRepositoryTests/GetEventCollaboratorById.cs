using AutoMapper;
using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Repositories;
using UnitTests.Builders;

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
    public async Task Should_ReturnEventCollaborators_When_EventCollaboratorAvailableWithGivenId()
    {
        _dbContext = await new EventCollaboratorRepositoryDBContext().GetDatabaseContext();

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

        User user = new UserBuilder()
                    .WithId(3)
                    .WithName("c")
                    .WithEmail("c")
                    .Build();

        EventCollaborator expectedResult = new EventCollaboratorBuilder()
                                              .WithId(3)
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Participant)
                                              .WithConfirmationStatus(ConfirmationStatus.Proposed)
                                              .WithEventDate(new DateOnly(2024,6,7))
                                              .WithProposedDuration(new Duration(1,2))
                                              .WithUser(user)
                                              .WithEventId(2)
                                              .Build();

        EventCollaborator? eventCollaboratorById =  await eventCollaboratorRepository.GetEventCollaboratorById(3);

        eventCollaboratorById.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public async Task Should_ReturnNull_When_EventCollaboratorNotAvailableWithGivenId()
    {
        _dbContext = await new EventCollaboratorRepositoryDBContext().GetDatabaseContext();

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

        EventCollaborator? eventCollaborator =  await eventCollaboratorRepository.GetEventCollaboratorById(5);

        eventCollaborator.Should().BeNull();
    }
}
