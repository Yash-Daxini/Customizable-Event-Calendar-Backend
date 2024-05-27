using AutoMapper;
using Core.Domain;
using Infrastructure.DataModels;

namespace Infrastructure.Profiles;

public class DateWiseParticipantsResolver : IValueResolver<EventDataModel, Event, List<EventCollaboratorsByDate>>
{
    private readonly IMapper _mapper;

    public DateWiseParticipantsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public List<EventCollaboratorsByDate> Resolve(EventDataModel source, Event destination, List<EventCollaboratorsByDate> destMember, ResolutionContext context)
    {
        return source.EventCollaborators
                     .GroupBy(eventCollaborator => eventCollaborator.EventDate)
                     .Select(group => new EventCollaboratorsByDate
                     {
                         EventDate = group.Key,
                         EventCollaborators = group.Select(eventCollaborator => _mapper.Map<EventCollaborator>(eventCollaborator)).ToList()
                     })
                     .ToList();
    }
}