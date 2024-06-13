using AutoMapper;
using Core.Entities;
using Core.Entities.Enums;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
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
        _eventCollaborator = new EventCollaborator()
        {
            Id = 1,
            EventId = 1,
            EventCollaboratorRole = EventCollaboratorRole.Organizer,
            ConfirmationStatus = ConfirmationStatus.Accept,
            EventDate = new DateOnly(),
            ProposedDuration = null,
            User = new()
            {
                Id = 1,
                Name = "Test",
                Email = "Test",
                Password = "Test",
            }
        };
    }

    [Fact]
    public async Task Should_ReturnActionResultOk_When_CollaborationAddedSuccessfully()
    {
        IActionResult actionResult = await _eventCollaborationController.AddEventCollaboration(_collaborationRequestDto);

        Assert.IsType<OkObjectResult>(actionResult);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_When_CollaborationOverlaps()
    {
        _sharedEventCollaborationService.AddCollaborator(_eventCollaborator).ThrowsForAnyArgs<CollaborationOverlapException>();

        IActionResult actionResult = await _eventCollaborationController.AddEventCollaboration(_collaborationRequestDto);

        Assert.IsType<BadRequestObjectResult>(actionResult);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_When_AlreadyCollaboratedOnEvent()
    {
        _sharedEventCollaborationService.AddCollaborator(_eventCollaborator).ThrowsForAnyArgs<UserAlreadyCollaboratedException>();

        IActionResult actionResult = await _eventCollaborationController.AddEventCollaboration(_collaborationRequestDto);

        Assert.IsType<BadRequestObjectResult>(actionResult);
    }
    
    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        _sharedEventCollaborationService.AddCollaborator(_eventCollaborator).ThrowsForAnyArgs<Exception>();

        IActionResult actionResult = await _eventCollaborationController.AddEventCollaboration(_collaborationRequestDto);

        Assert.IsType<ObjectResult>(actionResult);
    }


}
