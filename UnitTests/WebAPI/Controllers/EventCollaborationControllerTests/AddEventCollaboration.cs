using AutoMapper;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.EventCollaborationControllerTests;

public class AddEventCollaboration
{
    private readonly ISharedEventCollaborationService _sharedEventCollaborationService;
    private readonly IMapper _mapper;
    private readonly EventCollaborationController _eventCollaborationController;

    public AddEventCollaboration()
    {
        _sharedEventCollaborationService = Substitute.For<ISharedEventCollaborationService>();
        _mapper = Substitute.For<IMapper>();
        _eventCollaborationController = new EventCollaborationController(_sharedEventCollaborationService, _mapper);
    }

    [Fact]
    public async Task Should_ReturnActionResultOk_When_CollaborationAddedSuccessfully()
    {
        EventCollaborationRequestDto eventCollaborationRequestDto = Substitute.For<EventCollaborationRequestDto>();

        IActionResult actionResult = await _eventCollaborationController.AddEventCollaboration(eventCollaborationRequestDto);

        Assert.IsType<OkObjectResult>(actionResult);
    }
}
