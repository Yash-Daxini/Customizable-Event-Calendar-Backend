using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using FluentAssertions;
using Core.Entities.Enums;
using UnitTests.Builders.EntityBuilder;

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
        _dbContext = await new EventCollaboratorRepositoryDBContext().GetDatabaseContext();

        EventCollaboratorRepository eventCollaboratorRepository = new(_dbContext, _mapper);

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

        await eventCollaboratorRepository.Update(eventCollaborator);

        EventCollaborator? updatedEventCollaborator = await eventCollaboratorRepository.GetEventCollaboratorById(1);

        updatedEventCollaborator.Should().BeEquivalentTo(eventCollaborator);
    }
}
