using Core.Domain;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;

namespace Core.Services
{
    public class EventCollaboratorService : IEventCollaboratorService
    {
        private readonly IEventCollaboratorRepository _eventCollaboratorRepository;

        public EventCollaboratorService(IEventCollaboratorRepository eventCollaboratorRepository)
        {
            _eventCollaboratorRepository = eventCollaboratorRepository;
        }

        public Task<int> AddEventCollaborator(EventCollaborator eventCollaborator)
        {
            return _eventCollaboratorRepository.AddEventCollaborator(eventCollaborator);
        }

        public Task DeleteEventCollaborator(int eventCollaboratorId)
        {
            return _eventCollaboratorRepository.DeleteEventCollaborator(eventCollaboratorId);
        }

        public Task<int> UpdateEventCollaborator(EventCollaborator eventCollaborator)
        {
            return _eventCollaboratorRepository.UpdateEventCollaborator(eventCollaborator);
        }

        public Task DeleteEventCollaboratorsByEventId(int eventId)
        {
            return _eventCollaboratorRepository.DeleteEventCollaboratorsByEventId(eventId);
        }
    }
}
