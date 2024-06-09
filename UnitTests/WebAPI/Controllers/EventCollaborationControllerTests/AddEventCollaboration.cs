using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;
using WebAPI.Dtos;
using Xunit;

namespace UnitTests.WebAPI.Controllers.EventCollaborationControllerTests;

public class AddEventCollaboration : IClassFixture<AutoMapperFixture>
{
    private readonly ISharedEventCollaborationService _sharedEventCollaborationService;
    private readonly IMapper _mapper;
    private readonly EventCollaborationController _eventCollaborationController;

    public AddEventCollaboration(AutoMapperFixture autoMapperFixture)
    {
        _sharedEventCollaborationService = Substitute.For<ISharedEventCollaborationService>();
        _mapper = autoMapperFixture.Mapper;
        _eventCollaborationController = new EventCollaborationController(_sharedEventCollaborationService, _mapper);
    }

    [Fact]
    public async Task Should_ReturnActionResultOk_When_CollaborationAddedSuccessfully()
    {
        EventCollaborationRequestDto eventCollaborationRequestDto = Substitute.For<EventCollaborationRequestDto>();

        IActionResult actionResult = await _eventCollaborationController.AddEventCollaboration(eventCollaborationRequestDto);

        Assert.IsType<OkObjectResult>(actionResult);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_When_CollaborationOverlaps()
    {
        EventCollaborationRequestDto eventCollaborationRequestDto = Substitute.For<EventCollaborationRequestDto>();

        EventCollaborator eventCollaborator = Substitute.For<EventCollaborator>();

        _sharedEventCollaborationService.AddCollaborator(eventCollaborator).ThrowsForAnyArgs<CollaborationOverlapException>();

        IActionResult actionResult = await _eventCollaborationController.AddEventCollaboration(eventCollaborationRequestDto);

        Assert.IsType<BadRequestObjectResult>(actionResult);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_When_AlreadyCollaboratedOnEvent()
    {
        EventCollaborationRequestDto eventCollaborationRequestDto = Substitute.For<EventCollaborationRequestDto>();

        EventCollaborator eventCollaborator = Substitute.For<EventCollaborator>();

        _sharedEventCollaborationService.AddCollaborator(eventCollaborator).ThrowsForAnyArgs<UserAlreadyCollaboratedException>();

        IActionResult actionResult = await _eventCollaborationController.AddEventCollaboration(eventCollaborationRequestDto);

        Assert.IsType<BadRequestObjectResult>(actionResult);
    }
    
    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccured()
    {
        EventCollaborationRequestDto eventCollaborationRequestDto = Substitute.For<EventCollaborationRequestDto>();

        EventCollaborator eventCollaborator = Substitute.For<EventCollaborator>();

        _sharedEventCollaborationService.AddCollaborator(eventCollaborator).ThrowsForAnyArgs<Exception>();

        IActionResult actionResult = await _eventCollaborationController.AddEventCollaboration(eventCollaborationRequestDto);

        Assert.IsType<ObjectResult>(actionResult);
    }


}
