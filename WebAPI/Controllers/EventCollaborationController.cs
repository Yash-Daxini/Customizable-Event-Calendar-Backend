using Core.Domain;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCollaborationController : ControllerBase
    {
        private readonly ISharedEventCollaborationService _sharedEventCollaborationService;

        public EventCollaborationController(ISharedEventCollaborationService sharedEventCollaborationService)
        {
            _sharedEventCollaborationService = sharedEventCollaborationService;
        }

        [HttpPost("{eventId}")]
        public async Task<IActionResult> AddEventCollaboration([FromBody] Participant participant, [FromRoute] int eventId)
        {
            bool isAlreadyCollaborated = await _sharedEventCollaborationService
                                               .IsEventAlreadyCollaborated(participant, eventId);

            Event? collaborationOverlap = await _sharedEventCollaborationService
                                                .GetCollaborationOverlap(participant, eventId);

            if (isAlreadyCollaborated) 
                return BadRequest(new { message = "Already collaborated in this event" });


            if (collaborationOverlap is not null)
                return BadRequest(new { message = $"Overlaps with {collaborationOverlap.Title} at " +
                                                  $"{participant.EventDate} from {collaborationOverlap
                                                     .Duration.GetDurationInFormat()}" });

            await _sharedEventCollaborationService.AddCollaborator(participant, eventId);

            return Ok(new { message = "Successfully collaborated !" });
        }
    }
}
