using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class NonRecurringEventDateWiseEventCollaboratorsResolver : IValueResolver<NonRecurringEventRequestDto, Event, List<EventCollaboratorsByDate>>
{
    private readonly IMapper _mapper;

    public NonRecurringEventDateWiseEventCollaboratorsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public List<EventCollaboratorsByDate> Resolve(NonRecurringEventRequestDto source, Event destination, List<EventCollaboratorsByDate> destMember, ResolutionContext context)
    {
        return [new EventCollaboratorsByDate()
                {
                    EventDate = new DateOnly(),
                    EventCollaborators = _mapper.Map<List<EventCollaborator>>(source.EventCollaborators)
                }
               ];
    }
}
    