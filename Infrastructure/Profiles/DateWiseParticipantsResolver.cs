using AutoMapper;
using Core.Domain;
using Infrastructure.DataModels;

public class DateWiseParticipantsResolver : IValueResolver<EventDataModel, Event, List<ParticipantsByDate>>
{
    private readonly IMapper _mapper;

    public DateWiseParticipantsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public List<ParticipantsByDate> Resolve(EventDataModel source, Event destination, List<ParticipantsByDate> destMember, ResolutionContext context)
    {
        return source.EventCollaborators
                     .GroupBy(eventCollaborator => eventCollaborator.EventDate)
                     .Select(group => new ParticipantsByDate
                     {
                         EventDate = group.Key,
                         Participants = group.Select(eventCollaborator => _mapper.Map<Participant>(eventCollaborator)).ToList()
                     })
                     .ToList();
    }
}
