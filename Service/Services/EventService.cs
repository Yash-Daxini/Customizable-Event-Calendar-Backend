using System.Transactions;
using Core.Domain;
using Core.Interfaces;
using Infrastructure.Repositories;

namespace Core.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        private readonly IRecurrenceService _recurrenceService;

        private readonly IParticipantService _participantService;

        public EventService(IEventRepository eventRepository, IRecurrenceService recurrenceService, IParticipantService participantService)
        {
            _eventRepository = eventRepository;
            _recurrenceService = recurrenceService;
            _participantService = participantService;
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await _eventRepository.GetAllEvents();
        }

        public async Task<Event?> GetEventById(int eventId)
        {
            return await _eventRepository.GetEventsById(eventId);
        }

        public async Task<int> AddEvent(Event eventModel)
        {
            TransactionScope scope = new();

            int eventId = await _eventRepository.AddEvent(eventModel);

            eventModel.Id = eventId;

            List<Participant> participants = eventModel.DateWiseParticipants.First().Participants;

            _recurrenceService.ScheduleEvents(eventModel, participants);

            scope.Complete();

            return eventId;
        }

        public async Task<int> UpdateEvent(int eventId, Event eventModel)
        {
            int updatedEventId = await _eventRepository.UpdateEvent(eventId, eventModel);

            eventModel.Id = updatedEventId;

            await _participantService.DeleteParticipantsByEventId(eventId);

            List<Participant> participants = eventModel.DateWiseParticipants.First().Participants;

            _recurrenceService.ScheduleEvents(eventModel, participants);

            return updatedEventId;
        }

        public async Task DeleteEvent(int eventId)
        {
            await _participantService.DeleteParticipantsByEventId(eventId);

            await _eventRepository.DeleteEvent(eventId);
        }

        public async Task<List<Event>> GetEventsWithinGivenDates(DateOnly startDate, DateOnly endDate)
        {
            return [.._eventRepository
                  .GetEventsWithinGivenDate(startDate, endDate).Result
                  .Where(eventModel=>!eventModel.IsProposedEventToGiveResponse())];
        }

        public async Task<List<Event>> GetProposedEvents()
        {
            return [.. GetAllEvents().Result.Where(eventModel => eventModel.IsProposedEventToGiveResponse())
                                            .GroupBy(eventModel => eventModel.Id)
                                            .Select(groupedEvent => groupedEvent.First())];
        }
    }
}
