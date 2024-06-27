using AutoMapper;
using Core.Entities;
using Core.Entities.Enums;
using Core.Exceptions;
using Core.Interfaces.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using UnitTests.Builders;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.EventCollaborationControllerTests;

public class AddEventCollaboration : IClassFixture<AutoMapperFixture>
{
    private readonly ISharedEventCollaborationService _sharedEventCollaborationService;
    private readonly IMapper _mapper;

    private readonly EventCollaborationController _eventCollaborationController;

    private readonly CollaborationRequestDto _collaborationRequestDto;
    private readonly EventCollaborator _eventCollaborator;

    public AddEventCollaboration(AutoMapperFixture autoMapperFixture)
    {
        _sharedEventCollaborationService = Substitute.For<ISharedEventCollaborationService>();
        _mapper = autoMapperFixture.Mapper;
        _eventCollaborationController = new EventCollaborationController(_sharedEventCollaborationService, _mapper);

        _collaborationRequestDto = new CollaborationRequestDto()
        {
            Id = 1,
            EventId = 1,
            EventDate = new DateOnly()
        };

        User user = new UserBuilder(1)
                    .WithName("Test")
                    .WithEmail("Test")
                    .WithPassword("Test")
                    .Build();

        _eventCollaborator = new EventCollaboratorBuilder()
                             .WithId(1)
                             .WithEventId(1)
                             .WithEventDate(new DateOnly())
                             .WithEventCollaboratorRole(EventCollaboratorRole.Organizer)
                             .WithConfirmationStatus(ConfirmationStatus.Accept)
                             .WithProposedDuration(null)
                             .WithUser(user)
                             .Build();
    }

    [Fact]
    public async Task Should_Return_ActionResultOk_When_CollaborationAddedSuccessfully()
    {
        IActionResult actionResult = await _eventCollaborationController.AddEventCollaboration(_collaborationRequestDto);

        actionResult.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_CollaborationOverlaps()
    {
        _sharedEventCollaborationService.AddCollaborator(_eventCollaborator).ThrowsForAnyArgs(new CollaborationOverlapException(""));

        IActionResult actionResult = await _eventCollaborationController.AddEventCollaboration(_collaborationRequestDto);

        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_AlreadyCollaboratedOnEvent()
    {
        _sharedEventCollaborationService.AddCollaborator(_eventCollaborator).ThrowsForAnyArgs(new UserAlreadyCollaboratedException(""));

        IActionResult actionResult = await _eventCollaborationController.AddEventCollaboration(_collaborationRequestDto);

        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }
    
    [Fact]
    public async Task Should_Return_ServerError_When_SomeErrorOccurred()
    {
        _sharedEventCollaborationService.AddCollaborator(_eventCollaborator).ThrowsForAnyArgs<Exception>();

        IActionResult actionResult = await _eventCollaborationController.AddEventCollaboration(_collaborationRequestDto);

        actionResult.Should().BeOfType<ObjectResult>();
    }


}
