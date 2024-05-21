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

        public Task<int> AddParticipant(ParticipantModel participantModel, int eventId)
        {
            return _eventCollaboratorRepository.AddParticipant(participantModel, eventId);
        }

        public Task DeleteParticipant(int participantId)
        {
            return _eventCollaboratorRepository.DeleteParticipant(participantId);
        }

        public Task<int> UpdateParticipant(int participantId, ParticipantModel participantModel, int eventId)
        {
            return _eventCollaboratorRepository.UpdateParticipant(participantId, participantModel, eventId);
        }
    }
}
