using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class RecurringEventDateWiseEventCollaboratorsResolver : IValueResolver<RecurringEventRequestDto, Event, List<EventCollaboratorsByDate>>
{
    private readonly IMapper _mapper;

    public RecurringEventDateWiseEventCollaboratorsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public List<EventCollaboratorsByDate> Resolve(RecurringEventRequestDto source, Event destination, List<EventCollaboratorsByDate> destMember, ResolutionContext context)
    {
        return [new EventCollaboratorsByDate()
                {
                    EventDate = new DateOnly(),
                    EventCollaborators = _mapper.Map<List<EventCollaborator>>(source.EventCollaborators)
                }
               ];
    }
}
