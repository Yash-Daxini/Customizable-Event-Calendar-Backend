using Core.Entities;
using Core.Exceptions;
using Core.Extensions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using ArgumentNullException = Core.Exceptions.ArgumentNullException;

namespace Core.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    private readonly IRecurrenceService _recurrenceService;
    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IOverlappingEventService _overlappingEventService;
    private readonly ISharedCalendarService _sharedCalendarService;

    public EventService(IEventRepository eventRepository,
                        IRecurrenceService recurrenceService,
                        IEventCollaboratorService eventCollaboratorService,
                        IOverlappingEventService overlappingEventService,
                        ISharedCalendarService sharedCalendarService)
    {
        _eventRepository = eventRepository;
        _recurrenceService = recurrenceService;
        _eventCollaboratorService = eventCollaboratorService;
        _overlappingEventService = overlappingEventService;
        _sharedCalendarService = sharedCalendarService;
    }

    public async Task<List<Event>> GetAllEventsByUserId(int userId) => await _eventRepository.GetAllEventsByUserId(userId);

    public async Task<List<Event>> GetAllEventCreatedByUser(int userId)
    {
        List<Event> events = await _eventRepository.GetAllEventsByUserId(userId);

        return [..events
                 .Where(eventObj => eventObj.GetEventOrganizer().Id == userId)];
    }

    public async Task<Event> GetEventById(int eventId, int userId)
    {
        Event? eventObj = await _eventRepository.GetEventById(eventId);

        return eventObj is null
               ? throw new NotFoundException($"Event with id {eventId} not found.")
               : eventObj;
    }


    public async Task<int> AddNonRecurringEvent(Event eventModel, int userId)
    {
        if(eventModel is null)
            throw new ArgumentNullException($" Event can't be null");

        eventModel.MakeNonRecurringEvent();
        return await AddEvent(eventModel, userId);
    }

    public async Task<int> AddEvent(Event eventModel, int userId)
    {
        if (eventModel is null)
            throw new ArgumentNullException($" Event can't be null");

        CreateDateWiseEventCollaboratorList(eventModel);

        await HandleEventOverlap(eventModel, userId);

        eventModel.Id = await _eventRepository.Add(eventModel);

        return eventModel.Id;
    }


    public async Task UpdateEvent(Event eventModel, int userId)
    {
        if (eventModel is null)
            throw new ArgumentNullException($" Event can't be null");

        await GetEventById(eventModel.Id, userId);

        CreateDateWiseEventCollaboratorList(eventModel);

        await HandleEventOverlap(eventModel, userId);

        await _eventCollaboratorService.DeleteEventCollaboratorsByEventId(eventModel.Id, userId);

        await _eventRepository.Update(eventModel);
    }

    public async Task DeleteEvent(int eventId, int userId)
    {
        if (eventId is <= 0 )
            throw new ArgumentException($"Invalid event id");

        Event? eventObj = await _eventRepository.GetEventById(eventId) 
                          ?? throw new NotFoundException($"Event with id ${eventId} not present");

        await _eventRepository.Delete(eventObj);
    }

    public async Task<List<Event>> GetProposedEventsByUserId(int userId)
    {
        List<Event> events = await _eventRepository.GetAllEventsByUserId(userId);

        return events.Where(eventObj => eventObj.IsProposedEvent())
                     .ToList();
    }

    public async Task<List<Event>> GetNonProposedEventsByUserId(int userId)
    {
        List<Event> events = await _eventRepository.GetAllEventsByUserId(userId);

        return events.Where(eventObj => !eventObj.IsProposedEvent())
                     .ToList();
    }

    public async Task<List<Event>> GetEventsWithinGivenDatesByUserId(int userId, DateOnly startDate, DateOnly endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("start date can't be greater than end date.");

        return await _eventRepository.GetEventsWithinGivenDateByUserId(userId, startDate, endDate);
    }

    public async Task<List<Event>> GetEventsForDailyViewByUserId(int userId)
    {
        DateOnly today = DateTime.Today.ConvertToDateOnly();

        return await _eventRepository.GetEventsWithinGivenDateByUserId(userId, today, today);
    }

    public async Task<List<Event>> GetEventsForWeeklyViewByUserId(int userId)
    {
        DateOnly startDateOfWeek = DateTime.Today.GetStartDateOfWeek();
        DateOnly endDateOfWeek = DateTime.Today.GetEndDateOfWeek();

        return await _eventRepository.GetEventsWithinGivenDateByUserId(userId, startDateOfWeek, endDateOfWeek);
    }

    public async Task<List<Event>> GetEventsForMonthlyViewByUserId(int userId)
    {
        DateOnly startDateOfMonth = DateTime.Today.GetStartDateOfMonth();
        DateOnly endDateOfMonth = DateTime.Today.GetEndDateOfMonth();

        return await _eventRepository.GetEventsWithinGivenDateByUserId(userId, startDateOfMonth, endDateOfMonth);
    }

    public async Task<List<Event>> GetSharedEvents(int sharedCalendarId)
    {
        if (sharedCalendarId is <= 0)
            throw new ArgumentException($"Invalid shared calendar id");

        SharedCalendar? sharedCalendar = await _sharedCalendarService.GetSharedCalendarById(sharedCalendarId);

        if (sharedCalendar == null) return [];

        return await _eventRepository.GetSharedEvents(sharedCalendar);
    }

    private async Task HandleEventOverlap(Event eventModel, int userId)
    {
        List<Event> events = await _eventRepository.GetAllEventsByUserId(userId); 

        events = [..events
                   .Where(eventObj => eventObj.Id != eventModel.Id)];

        string? overlapEventInformation = _overlappingEventService
                                          .GetOverlappedEventInformation(eventModel, events);

        if (overlapEventInformation is not null)
            throw new EventOverlapException($"{overlapEventInformation}");
    }

    private void CreateDateWiseEventCollaboratorList(Event eventModel)
    {
        List<DateOnly> occurrences = _recurrenceService.GetOccurrencesOfEvent(eventModel.RecurrencePattern);

        eventModel.CreateDateWiseEventCollaboratorsList(occurrences);
    }
}
