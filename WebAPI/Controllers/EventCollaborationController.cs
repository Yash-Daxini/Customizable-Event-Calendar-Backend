using AutoMapper;
using Core.Domain;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
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
        [ServiceFilter(typeof(ValidationFilter<EventCollaboratorDto>))]
        public async Task<IActionResult> AddEventCollaboration([FromBody] EventCollaboratorDto eventCollaboratorDto)
        {
            try
            {
                await _sharedEventCollaborationService.AddCollaborator(_mapper.Map<EventCollaborator>(eventCollaboratorDto));

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
}
