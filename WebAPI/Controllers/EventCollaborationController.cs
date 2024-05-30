using AutoMapper;
using Core.Domain.Models;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventCollaborationController : ControllerBase
{
    private readonly ISharedEventCollaborationService _sharedEventCollaborationService;
    private readonly IMapper _mapper;

    public EventCollaborationController(ISharedEventCollaborationService sharedEventCollaborationService, IMapper mapper)
    {
        _sharedEventCollaborationService = sharedEventCollaborationService;
        _mapper = mapper;
    }

    [HttpPost("")]
    public async Task<IActionResult> AddEventCollaboration([FromBody] EventCollaborationRequestDto eventCollaborationRequestDto)
    {
        try
        {
            await _sharedEventCollaborationService.AddCollaborator(_mapper.Map<EventCollaborator>(eventCollaborationRequestDto));

            return Ok(new { message = "Successfully collaborated !" });
        }
        catch (CollaborationOverlapException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (UserAlreadyCollaboratedException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
