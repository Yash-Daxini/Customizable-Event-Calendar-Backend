using AutoMapper;
using Core.Domain;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;

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
        public async Task<IActionResult> AddEventCollaboration([FromBody] ParticipantDto participant)
        {
            try
            {
                await _sharedEventCollaborationService.AddCollaborator(_mapper.Map<Participant>(participant));

                return Ok(new { message = "Successfully collaborated !" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
