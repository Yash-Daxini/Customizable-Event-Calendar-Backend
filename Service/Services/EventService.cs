using Core.Domain;
using Core.Interfaces;
using Infrastructure.Repositories;

namespace Core.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly RecurrenceService _recurrenceService;

        public EventService(IEventRepository eventRepository,RecurrenceService recurrenceService)
        {
            _eventRepository = eventRepository;
            _recurrenceService = recurrenceService;
        }

        public Task<List<EventModel>> GetAllEvents()
        {
            return _eventRepository.GetAllEvents();
        }

        public Task<EventModel?> GetEventById(int eventId)
        {
            return _eventRepository.GetEventsById(eventId);
        }

        public Task<int> AddEvent(EventModel eventModel)
        {
            Task<int> eventId = _eventRepository.AddEvent(eventModel);

            eventModel.Id = eventId.Result;

            List<ParticipantModel> participants = eventModel.DateWiseParticipants.First().Participants;

            _recurrenceService.ScheduleEvents(eventModel,participants);

            return eventId;
        }

        public Task<int> UpdateEvent(int eventId, EventModel eventModel)
        {
            Task<int> updatedEventId = _eventRepository.UpdateEvent(eventId, eventModel);

            List<ParticipantModel> participants = eventModel.DateWiseParticipants.First().Participants;

            _recurrenceService.ScheduleEvents(eventModel, participants);

            return updatedEventId;
        }

        public Task DeleteEvent(int eventId)
        {
            return _eventRepository.DeleteEvent(eventId);
        }
    }
}
