using Core.Domain;
using Core.Exceptions;
using Core.Extensions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;

namespace Core.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        private readonly IRecurrenceService _recurrenceService;
        private readonly IEventCollaboratorService _participantService;
        private readonly IOverlappingEventService _overlappingEventService;
        private readonly ISharedCalendarService _sharedCalendarService;

        public EventService(IEventRepository eventRepository,
                            IRecurrenceService recurrenceService,
                            IEventCollaboratorService participantService,
                            IOverlappingEventService overlappingEventService,
                            ISharedCalendarService sharedCalendarService)
        {
            _eventRepository = eventRepository;
            _recurrenceService = recurrenceService;
            _participantService = participantService;
            _overlappingEventService = overlappingEventService;
            _sharedCalendarService = sharedCalendarService;
        }

        public async Task<List<Event>> GetAllEventsByUserId(int userId) => await _eventRepository.GetAllEventsByUserId(userId);

        public async Task<Event?> GetEventById(int eventId)
        {
            Event? eventObj = await _eventRepository.GetEventsById(eventId);

            return eventObj is null 
                   ? throw new NotFoundException($"Event with id {eventId} not found.") 
                   : eventObj;
        }

        public async Task<int> AddEvent(Event eventModel)
        {
            List<DateOnly> occurrences = _recurrenceService.GetOccurrencesOfEvent(eventModel);

            MakeDateWiseParticipantListFromOccurrences(eventModel, occurrences);

            OverlapEventData? overlapEventData = _overlappingEventService
                                                 .GetOverlappedEventInformation(eventModel,
                                                                                GetAllEventsByUserId(eventModel.Id).Result);

            if (overlapEventData is not null)
                throw new EventOverlapException($"{overlapEventData.GetOverlapMessage()}");

            eventModel.Id = await _eventRepository.AddEvent(eventModel);

            return eventModel.Id;
        }

        private void MakeDateWiseParticipantListFromOccurrences(Event eventModel, List<DateOnly> occurrences)
        {
            List<EventCollaborator> participants = eventModel.DateWiseParticipants.First().EventCollaborators;

            List<EventCollaboratorsByDate> participantsByDates = [];

            foreach (DateOnly occurrence in occurrences)
            {
                participantsByDates.Add(new EventCollaboratorsByDate()
                {
                    EventDate = occurrence,
                    EventCollaborators = participants
                });
            }

            eventModel.DateWiseParticipants = participantsByDates;
        }

        public async Task UpdateEvent(Event eventModel)
        {
            await GetEventById(eventModel.Id);

            List<DateOnly> occurrences = _recurrenceService.GetOccurrencesOfEvent(eventModel);

            MakeDateWiseParticipantListFromOccurrences(eventModel, occurrences);

            OverlapEventData? overlapEventData = _overlappingEventService
                                                 .GetOverlappedEventInformation(eventModel,
                                                                                GetAllEventsByUserId(eventModel.Id).Result);

            if (overlapEventData is not null)
                throw new EventOverlapException($"{overlapEventData.GetOverlapMessage()}");

            await _participantService.DeleteEventCollaboratorsByEventId(eventModel.Id);

            await _eventRepository.UpdateEvent(eventModel);
        }

        public async Task DeleteEvent(int eventId)
        {
            await GetEventById(eventId);

            await _eventRepository.DeleteEvent(eventId);
        }

        public async Task<List<Event>> GetProposedEventsByUserId(int userId)
        {
            List<Event> events = await _eventRepository.GetProposedEventsByUserId(userId);

            return events.Where(eventObj => eventObj.IsProposedEventToGiveResponse())
                         .ToList();
        }

        public async Task<List<Event>> GetNonProposedEventsByUserId(int userId)
        {
            List<Event> events = await GetAllEventsByUserId(userId);

            return events.Where(eventObj => !eventObj.IsProposedEventToGiveResponse())
                         .ToList();
        }

        public async Task<List<Event>> GetEventsWithinGivenDatesByUserId(int userId, DateOnly startDate, DateOnly endDate) =>
               await _eventRepository.GetEventsWithinGivenDateByUserId(userId, startDate, endDate);

        public async Task<List<Event>> GetEventsForDailyViewByUserId(int userId)
        {
            DateOnly today = DateTime.Today.ConvertToDateOnly();
            return await GetEventsWithinGivenDatesByUserId(userId, today, today);
        }

        public async Task<List<Event>> GetEventsForWeeklyViewByUserId(int userId)
        {
            DateOnly startDateOfWeek = DateTime.Today.GetStartDateOfWeek();
            DateOnly endDateOfWeek = DateTime.Today.GetEndDateOfWeek();

            return await GetEventsWithinGivenDatesByUserId(userId, startDateOfWeek, endDateOfWeek);
        }

        public async Task<List<Event>> GetEventsForMonthlyViewByUserId(int userId)
        {
            DateOnly startDateOfMonth = DateTime.Today.GetStartDateOfMonth();
            DateOnly endDateOfMonth = DateTime.Today.GetEndDateOfMonth();

            return await GetEventsWithinGivenDatesByUserId(userId, startDateOfMonth, endDateOfMonth);
        }

        public async Task<List<Event>> GetSharedEvents(int sharedCalendarId)
        {
            SharedCalendar? sharedCalendar = await _sharedCalendarService.GetSharedCalendarById(sharedCalendarId);
            if (sharedCalendar == null) return [];
            return await _eventRepository.GetSharedEvents(sharedCalendar);
        }
    }
}
