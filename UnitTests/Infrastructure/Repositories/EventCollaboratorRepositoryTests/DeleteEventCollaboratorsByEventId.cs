using AutoMapper;
using Core.Entities;
using Core.Entities.Enums;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Repositories;
using UnitTests.Builders;

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
        _dbContext = await new EventCollaboratorRepositoryDBContext().GetDatabaseContext();

        User user = new UserBuilder()
                    .WithId(1)
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
