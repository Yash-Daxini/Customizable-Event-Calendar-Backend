using AutoMapper;
using Core.Entities;
using Core.Interfaces.IServices;
using FluentAssertions;
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
    private readonly EventCollaboratorConfirmationDto _eventCollaboratorConfirmationDto;

    public AddEventCollaboratorResponse(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _eventCollaboratorService = Substitute.For<IEventCollaboratorService>();
        _eventCollaboratorController = new EventCollaboratorController(_mapper, _eventCollaboratorService); 
        _eventCollaboratorConfirmationDto = new EventCollaboratorConfirmationDto()
        {
            Id = 1,
            EventId = 1,
            UserId = 1,
            ConfirmationStatus = "Accept",
            ProposedDuration = null
        };
    }

    [Fact]
    public async Task Should_AddEventCollaboratorResponse_When_EventCollaboratorResponseGiven()
    {
        IActionResult actionResult  = await _eventCollaboratorController.AddEventCollaboratorResponse(_eventCollaboratorConfirmationDto);

        actionResult.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        EventCollaborator eventCollaborator = Substitute.For<EventCollaborator>();

        _eventCollaboratorService.UpdateEventCollaborator(eventCollaborator).ThrowsAsyncForAnyArgs<Exception>();

        IActionResult actionResult  = await _eventCollaboratorController.AddEventCollaboratorResponse(_eventCollaboratorConfirmationDto);

        actionResult.Should().BeOfType<ObjectResult>();
    }


}
