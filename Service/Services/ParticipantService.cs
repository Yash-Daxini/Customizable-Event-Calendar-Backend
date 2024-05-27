using Core.Domain;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;

namespace Core.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IEventCollaboratorRepository _eventCollaboratorRepository;

        public ParticipantService(IEventCollaboratorRepository eventCollaboratorRepository)
        {
            _eventCollaboratorRepository = eventCollaboratorRepository;
        }

        public Task<int> AddParticipant(Participant participantModel)
        {
            return _eventCollaboratorRepository.AddParticipant(participantModel);
        }

        public async Task AddParticipants(List<Participant> participants)
        {
            await _eventCollaboratorRepository.AddParticipants(participants);
        }

        public Task DeleteParticipant(int participantId)
        {
            return _eventCollaboratorRepository.DeleteParticipant(participantId);
        }

        public Task<int> UpdateParticipant(int participantId, Participant participantModel)
        {
            return _eventCollaboratorRepository.UpdateParticipant(participantId, participantModel);
        }

        public Task DeleteParticipantsByEventId(int eventId)
        {
            return _eventCollaboratorRepository.DeleteParticipantsByEventId(eventId);
        }
    }
}
