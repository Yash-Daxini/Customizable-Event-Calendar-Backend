using Infrastructure.DataModels;
using Core.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces.IRepositories;

namespace Infrastructure.Repositories
{
    public class EventCollaboratorRepository : IEventCollaboratorRepository
    {
        private readonly DbContextEventCalendar _dbContext;

        private readonly IMapper _mapper;

        public EventCollaboratorRepository(DbContextEventCalendar dbContextEvent, IMapper mapper)
        {
            _dbContext = dbContextEvent;
            _mapper = mapper;
        }

        public async Task<List<Participant>> GetAllParticipants()
        {
            return await _dbContext.EventCollaborators
                        .ProjectTo<Participant>(_mapper.ConfigurationProvider)
                        .ToListAsync();
        }

        public async Task<Participant?> GetParticipantById(int participantId)
        {
            return await _dbContext
                        .EventCollaborators
                        .Where(participant => participant.Id == participantId)
                        .ProjectTo<Participant>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
        }

        public async Task<int> AddParticipant(Participant participantModel)
        {
            EventCollaboratorDataModel eventCollaborator = _mapper.Map<EventCollaboratorDataModel>(participantModel);

            _dbContext.Attach(eventCollaborator.User);

            _dbContext.EventCollaborators.Add(eventCollaborator);

            await _dbContext.SaveChangesAsync();

            return eventCollaborator.Id;
        }

        public async Task AddParticipants(List<Participant> participants)
        {
            List<EventCollaboratorDataModel> eventCollaboratorsToAdd = _mapper.Map<List<EventCollaboratorDataModel>>(participants);

            foreach (var eventCollaborator in eventCollaboratorsToAdd)
            {
                _dbContext.Attach(eventCollaborator.User);
            }

            _dbContext.EventCollaborators.AddRange(eventCollaboratorsToAdd);

            await _dbContext.SaveChangesAsync();

        }

        public async Task<int> UpdateParticipant(int participantId, Participant participantModel)
        {
            EventCollaboratorDataModel eventCollaborator = _mapper.Map<EventCollaboratorDataModel>(participantModel);

            eventCollaborator.Id = participantId;

            _dbContext.EventCollaborators.Update(eventCollaborator);

            await _dbContext.SaveChangesAsync();

            return eventCollaborator.Id;
        }

        public async Task DeleteParticipant(int participantId)
        {
            EventCollaboratorDataModel eventCollaborator = new()
            {
                Id = participantId,
            };

            _dbContext.Remove(eventCollaborator);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteParticipantsByEventId(int eventId)
        {
            List<EventCollaboratorDataModel> eventCollaboratorsToDelete = [.._dbContext
                                                                          .EventCollaborators
                                                                          .Where(eventcollaborator => eventcollaborator.EventId == eventId)];

            _dbContext.RemoveRange(eventCollaboratorsToDelete);

            await _dbContext.SaveChangesAsync();
        }
    }
}
