using Core.Interfaces;
using Core.Domain;
using Infrastructure.Repositories;

namespace Core.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IEventCollaboratorRepository _eventCollaboratorRepository;

        public ParticipantService(IEventCollaboratorRepository eventCollaboratorRepository)
        {
            _eventCollaboratorRepository = eventCollaboratorRepository;
        }

        public async Task<int> AddParticipant(Participant participantModel, int eventId)
        {
            return await _eventCollaboratorRepository.AddParticipant(participantModel, eventId);
        }

        public async Task AddParticipants(List<Participant> participants, int eventId)
        {
            await _eventCollaboratorRepository.AddParticipants(participants, eventId);
        }

        public async Task DeleteParticipant(int participantId)
        {
            await _eventCollaboratorRepository.DeleteParticipant(participantId);
        }

        public async Task<int> UpdateParticipant(int participantId, Participant participantModel, int eventId)
        {
            return await _eventCollaboratorRepository.UpdateParticipant(participantId, participantModel, eventId);
        }

        public async Task DeleteParticipantsByEventId(int eventId)
        {
            await _eventCollaboratorRepository.DeleteParticipantsByEventId(eventId);
        }
    }
}
