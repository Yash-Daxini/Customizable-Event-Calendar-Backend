using AutoMapper;
using Core.Entities;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.EventCollaboratorControllerTests;

public class AddEventCollaboratorResponse : IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;
    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly EventCollaboratorController _eventCollaboratorController;

    public AddEventCollaboratorResponse(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
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
    
    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccured()
    {
        EventCollaboratorConfirmationDto eventCollaboratorConfirmationDto = Substitute.For<EventCollaboratorConfirmationDto>();

        EventCollaborator eventCollaborator = Substitute.For<EventCollaborator>();

        _eventCollaboratorService.UpdateEventCollaborator(eventCollaborator).ThrowsAsyncForAnyArgs<Exception>();

        IActionResult actionResult  = await _eventCollaboratorController.AddEventCollaboratorResponse(eventCollaboratorConfirmationDto);

        Assert.IsType<ObjectResult>(actionResult);
    }


}
