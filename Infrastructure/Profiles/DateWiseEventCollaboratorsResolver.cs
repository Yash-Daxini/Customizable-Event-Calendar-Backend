using AutoMapper;
using Core.Domain.Models;
using Infrastructure.DataModels;

namespace Infrastructure.Profiles;

public class DateWiseEventCollaboratorsResolver : IValueResolver<EventDataModel, Event, List<EventCollaboratorsByDate>>
{
    private readonly IMapper _mapper;

    public DateWiseEventCollaboratorsResolver(IMapper mapper)
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