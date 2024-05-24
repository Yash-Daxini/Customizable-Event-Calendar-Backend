using Infrastructure.DataModels;
using Core.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Infrastructure.Repositories
{
    public class EventCollaboratorRepository : IEventCollaboratorRepository
    {
        private readonly DbContextEventCalendar _dbContextEventCalendar;

        private readonly IMapper _mapper;

        public EventCollaboratorRepository(DbContextEventCalendar dbContextEvent, IMapper mapper)
        {
            _dbContextEventCalendar = dbContextEvent;
            _mapper = mapper;
        }

        public async Task<List<Participant>> GetAllParticipants()
        {
            return await _dbContextEventCalendar.EventCollaborators
                        .ProjectTo<Participant>(_mapper.ConfigurationProvider)
                        .ToListAsync();
        }

        public async Task<Participant?> GetParticipantById(int participantId)
        {
            return await _dbContextEventCalendar
                        .EventCollaborators
                        .Where(participant => participant.Id == participantId)
                        .Select(eventCollaborator => _mapper.Map<Participant>(eventCollaborator))
                        .FirstOrDefaultAsync();
        }

        public async Task<int> AddParticipant(Participant participantModel, int eventId)
        {
            EventCollaboratorDataModel eventCollaborator = _mapper.Map<EventCollaboratorDataModel>(participantModel, opt =>
            {
                opt.Items["EventId"] = eventId;
            });

            _dbContextEventCalendar.Attach(eventCollaborator.User);

            _dbContextEventCalendar.EventCollaborators.Add(eventCollaborator);

            await _dbContextEventCalendar.SaveChangesAsync();

            return eventCollaborator.Id;
        }

        public async Task AddParticipants(List<Participant> participants, int eventId)
        {
            List<EventCollaboratorDataModel> eventCollaboratorsToAdd = [..participants
                                                                       .Select(participant => _mapper.Map<EventCollaboratorDataModel>(participant, opt =>
                                                                        {
                                                                                    opt.Items["EventId"] = eventId;
                                                                        }))];

            foreach (var eventCollaborator in eventCollaboratorsToAdd)
            {
                _dbContextEventCalendar.Attach(eventCollaborator.User);
            }

            _dbContextEventCalendar.EventCollaborators.AddRange(eventCollaboratorsToAdd);

            await _dbContextEventCalendar.SaveChangesAsync();

        }

        public async Task<int> UpdateParticipant(int participantId, Participant participantModel, int eventId)
        {
            EventCollaboratorDataModel eventCollaborator = _mapper.Map<EventCollaboratorDataModel>(participantModel, opt =>
            {
                opt.Items["EventId"] = eventId;
            });

            eventCollaborator.Id = participantId;

            _dbContextEventCalendar.EventCollaborators.Update(eventCollaborator);

            await _dbContextEventCalendar.SaveChangesAsync();

            return eventCollaborator.Id;
        }

        public async Task DeleteParticipant(int participantId)
        {
            EventCollaboratorDataModel eventCollaborator = new()
            {
                Id = participantId,
            };

            _dbContextEventCalendar.Remove(eventCollaborator);

            await _dbContextEventCalendar.SaveChangesAsync();
        }

        public async Task DeleteParticipantsByEventId(int eventId)
        {
            List<EventCollaboratorDataModel> eventCollaboratorsToDelete = [.._dbContextEventCalendar
                                                                          .EventCollaborators
                                                                          .Where(eventcollaborator => eventcollaborator.EventId == eventId)];

            _dbContextEventCalendar.RemoveRange(eventCollaboratorsToDelete);

            await _dbContextEventCalendar.SaveChangesAsync();
        }
    }
}
