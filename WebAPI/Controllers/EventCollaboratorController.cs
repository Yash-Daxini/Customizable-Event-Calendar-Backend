using AutoMapper;
using Core.Entities;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventCollaboratorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEventCollaboratorService _eventCollaboratorService;

        public EventCollaboratorController(IMapper mapper,
                                           IEventCollaboratorService eventCollaboratorService)
        {
            _mapper = mapper;
            _eventCollaboratorService = eventCollaboratorService;
        }

        [HttpPut("{eventCollaboratorId}/response")]
        public async Task<ActionResult> AddEventCollaboratorResponse([FromBody] EventCollaboratorConfirmationDto eventCollaboratorConfirmationDto)
        {
            try
            {
                int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());
                eventCollaboratorConfirmationDto.UserId = userId;

                EventCollaborator eventCollaborator = _mapper.Map<EventCollaborator>(eventCollaboratorConfirmationDto);

                await _eventCollaboratorService.UpdateEventCollaborator(eventCollaborator);

                return Ok(new { Message = "Updated Response successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
