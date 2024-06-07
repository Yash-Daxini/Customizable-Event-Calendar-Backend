using AutoMapper;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.EventCollaboratorControllerTests;

public class AddEventCollaboratorResponse
{
    private readonly IMapper _mapper;
    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly EventCollaboratorController _eventCollaboratorController;

    public AddEventCollaboratorResponse()
    {
        _mapper = Substitute.For<IMapper>();
        _eventCollaboratorService = Substitute.For<IEventCollaboratorService>();
        _eventCollaboratorController = new EventCollaboratorController(_mapper, _eventCollaboratorService); 
    }

    [Fact]
    public async Task Should_AddEventCollaboratorResponse_When_EventCollaboratorResponseGiven()
    {
        EventCollaboratorConfirmationDto eventCollaboratorConfirmationDto = Substitute.For<EventCollaboratorConfirmationDto>();

        IActionResult actionResult  = await _eventCollaboratorController.AddEventCollaboratorResponse(eventCollaboratorConfirmationDto);

        Assert.IsType<OkObjectResult>(actionResult);
    }
}
