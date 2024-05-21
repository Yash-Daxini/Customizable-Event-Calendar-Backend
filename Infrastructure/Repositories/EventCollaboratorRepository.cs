using Infrastructure.DataModels;
using Core.Domain;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventCollaboratorRepository : IEventCollaboratorRepository
    {
        private readonly DbContextEventCalendar _dbContextEvent;

        private readonly ParticipantMapper _participantMapper;

        public EventCollaboratorRepository(DbContextEventCalendar dbContextEvent,ParticipantMapper participantMapper)
        {
            _dbContextEvent = dbContextEvent;
            _participantMapper = participantMapper;
        }

        public async Task<List<ParticipantModel>> GetAllParticipants()
        {
            return await _dbContextEvent.EventCollaborators.Select(eventCollaborator => _participantMapper.MapEventCollaboratorToParticipantModel(eventCollaborator))
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

            await _dbContextEvent.EventCollaborators.AddAsync(eventCollaborator);

            await _dbContextEvent.SaveChangesAsync();

            return eventCollaborator.Id;
        }

        public async Task<int> UpdateParticipant(int participantId, ParticipantModel participantModel, int eventId)
        {
            EventCollaborator eventCollaborator = _participantMapper.MapParticipantModelToEventCollaborator(participantModel, eventId);

            eventCollaborator.Id = participantId;

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
