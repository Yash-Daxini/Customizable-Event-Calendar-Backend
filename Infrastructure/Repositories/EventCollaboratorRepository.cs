using Infrastructure.DataModels;
using Infrastructure.DomainEntities;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventCollaboratorRepository : IEventCollaboratorRepository
    {
        private readonly DbContextEvent _dbContextEvent;

        private readonly ParticipantMapper _participantMapper = new();

        public EventCollaboratorRepository(DbContextEvent dbContextEvent)
        {
            _dbContextEvent = dbContextEvent;
        }

        public async Task<List<ParticipantModel>> GetAllParticipants()
        {
            return await _dbContextEvent.EventCollaborators.Select(eventCollaborator => _participantMapper.MapEventCollaboratorToParticipantModel(eventCollaborator))
                                                           .ToListAsync();
        }

        public async Task<List<EventCollaborator>> GetAllEventCollaboratorsByEventId(int eventId)
        {
            return await _dbContextEvent.EventCollaborators.Select(eventCollaborator => eventCollaborator)
                                                           .Where(eventCollaborator => eventCollaborator.EventId == eventId)
                                                           .ToListAsync();
        }

        public async Task<ParticipantModel?> GetParticipantById(int bookId)
        {
            return await _dbContextEvent.EventCollaborators.Where(book => book.Id == bookId)
                                                .Select(eventCollaborator => _participantMapper.MapEventCollaboratorToParticipantModel(eventCollaborator))
                                                .FirstOrDefaultAsync();
        }

        public async Task<int> AddParticipant(ParticipantModel participantModel, int eventId)
        {
            EventCollaborator eventCollaborator = _participantMapper.MapParticipantModelToEventCollaborator(participantModel, eventId);

            _dbContextEvent.EventCollaborators.Add(eventCollaborator);

            await _dbContextEvent.SaveChangesAsync();

            return eventCollaborator.Id;
        }

        public async Task<int> UpdateParticipant(ParticipantModel participantModel, int eventId)
        {
            EventCollaborator eventCollaborator = _participantMapper.MapParticipantModelToEventCollaborator(participantModel, eventId);

            _dbContextEvent.EventCollaborators.Update(eventCollaborator);

            await _dbContextEvent.SaveChangesAsync();

            return eventCollaborator.Id;
        }

        public async Task DeleteParticipant(int participantId)
        {
            EventCollaborator eventCollaborator = new()
            {
                Id = participantId,
            };

            _dbContextEvent.Remove(eventCollaborator);

            await _dbContextEvent.SaveChangesAsync();
        }
    }
}
