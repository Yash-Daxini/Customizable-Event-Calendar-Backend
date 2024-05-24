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
        private readonly IOverlappingEventService _overlappingEventService;
        private readonly ISharedCalendarService _sharedCalendarService;

        public EventService(IEventRepository eventRepository,
                            IRecurrenceService recurrenceService,
                            IParticipantService participantService,
                            IOverlappingEventService overlappingEventService,
                            ISharedCalendarService sharedCalendarService)
        {
            _eventRepository = eventRepository;
            _recurrenceService = recurrenceService;
            _participantService = participantService;
            _overlappingEventService = overlappingEventService;
            _sharedCalendarService = sharedCalendarService;
        }

        public async Task<List<Event>> GetAllEvents() => await _eventRepository.GetAllEvents();

        public async Task<Event?> GetEventById(int eventId) => await _eventRepository.GetEventsById(eventId);

        public async Task<int> AddEvent(Event eventModel)
        {
            List<DateOnly> occurrences = _recurrenceService.GetOccurrencesOfEvent(eventModel);

            OverlapEventData? overlapEventData = _overlappingEventService
                                                 .GetOverlappedEventInformation(eventModel,
                                                                                GetEventsByUserId(eventModel.Id).Result,
                                                                                occurrences,
                                                                                true,
                                                                                eventModel.GetEventOrganizer().Id);

            if (overlapEventData is not null) return 0;

            int eventId = await _eventRepository.AddEvent(eventModel);

            eventModel.Id = eventId;

            List<Participant> participants = eventModel.DateWiseParticipants.First().Participants;

            await _recurrenceService.ScheduleEvents(eventModel, participants);

            return eventId;
        }

        public async Task<int> UpdateEvent(int eventId, Event eventModel)
        {
            List<DateOnly> occurrences = _recurrenceService.GetOccurrencesOfEvent(eventModel);

            eventModel.Id = eventId;

            OverlapEventData? overlapEventData = _overlappingEventService
                                                 .GetOverlappedEventInformation(eventModel,
                                                                                GetEventsByUserId(eventModel.Id).Result,
                                                                                occurrences,
                                                                                false,
                                                                                eventModel.GetEventOrganizer().Id);

            if (overlapEventData is not null) return 0;

            int updatedEventId = await _eventRepository.UpdateEvent(eventId, eventModel);

            eventModel.Id = updatedEventId;

            await _participantService.DeleteParticipantsByEventId(eventId);

            List<Participant> participants = eventModel.DateWiseParticipants.First().Participants;

            await _recurrenceService.ScheduleEvents(eventModel, participants);

            return updatedEventId;
        }

        public async Task DeleteEvent(int eventId)
        {
            await _participantService.DeleteParticipantsByEventId(eventId);

            await _eventRepository.DeleteEvent(eventId);
        }

        public async Task<List<Event>> GetProposedEvents()
        {
            List<Event> events = await _eventRepository.GetProposedEvents();

            return events.Where(eventObj => eventObj.IsProposedEventToGiveResponse())
                         .ToList();
        }

        public async Task<List<Event>> GetEventsByUserId(int userId) => await _eventRepository.GetEventsByUserId(userId);

        public async Task<List<Event>> GetNonProposedEventsByUserId(int userId)
        {
            List<Event> events = await GetEventsByUserId(userId);

            return events.Where(eventObj => !eventObj.IsProposedEventToGiveResponse())
                         .ToList();
        }

        public async Task<List<Event>> GetEventsWithinGivenDates(DateOnly startDate, DateOnly endDate) =>
               await _eventRepository.GetEventsWithinGivenDate(startDate, endDate);

        public async Task<List<Event>> GetEventsForDailyView()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            return await GetEventsWithinGivenDates(today, today);
        }

        public async Task<List<Event>> GetEventsForWeeklyView()
        {
            DateOnly startDateOfWeek = DateTimeService.GetStartDateOfWeek(DateTime.Today);
            DateOnly endDateOfWeek = DateTimeService.GetEndDateOfWeek(DateTime.Today);

            return await GetEventsWithinGivenDates(startDateOfWeek, endDateOfWeek);
        }

        public async Task<List<Event>> GetEventsForMonthlyView()
        {
            DateOnly startDateOfMonth = DateTimeService.GetStartDateOfMonth(DateTime.Today);
            DateOnly endDateOfMonth = DateTimeService.GetEndDateOfMonth(DateTime.Today);

            return await GetEventsWithinGivenDates(startDateOfMonth, endDateOfMonth);
        }

        public async Task<List<Event>> GetSharedEvents(int sharedCalendarId)
        {
            SharedCalendar? sharedCalendar = await _sharedCalendarService.GetSharedCalendarById(sharedCalendarId);
            if (sharedCalendar == null) return [];
            return await _eventRepository.GetSharedEvents(sharedCalendar);
        }
    }
}
