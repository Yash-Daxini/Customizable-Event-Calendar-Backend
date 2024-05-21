using Core.Interfaces;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpPost("{eventId}")]
        public async Task<ActionResult> AddParticipant([FromBody] ParticipantModel participantModel, [FromRoute] int eventId)
        {
            int addedUserId = await _participantService.AddParticipant(participantModel, eventId);
            return Ok(addedUserId);
        }

        [HttpPut("{participantId}/eventId/{eventId}")]
        public async Task<ActionResult> UpdateParticipant([FromRoute] int participantId,
                                                          [FromBody] ParticipantModel participantModel,
                                                          [FromRoute] int eventId)
        {
            int addedParticipantId = await _participantService.UpdateParticipant(participantId, participantModel, eventId);
            return Ok(addedParticipantId);
        }

        [HttpDelete("{participantId}")]
        public async Task<ActionResult> DeleteParticipant([FromRoute] int participantId)
        {
            await _participantService.DeleteParticipant(participantId);
            return Ok();
        }
    }
}
