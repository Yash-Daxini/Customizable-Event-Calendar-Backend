using AutoMapper;
using Core.Domain;
using Infrastructure.DataModels;

public class DateWiseParticipantsResolver : IValueResolver<Event, EventModel, List<ParticipantsByDate>>
{
    private readonly IMapper _mapper;

    public DateWiseParticipantsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public List<ParticipantsByDate> Resolve(Event source, EventModel destination, List<ParticipantsByDate> destMember, ResolutionContext context)
    {
        return source.EventCollaborators
                     .GroupBy(eventCollaborator => eventCollaborator.EventDate)
                     .Select(group => new ParticipantsByDate
                     {
                         EventDate = group.Key,
                         Participants = group.Select(eventCollaborator => _mapper.Map<ParticipantModel>(eventCollaborator)).ToList()
                     })
                     .ToList();
    }
}
