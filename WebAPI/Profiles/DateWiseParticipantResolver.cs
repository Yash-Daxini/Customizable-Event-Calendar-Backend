using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class DateWiseEventCollaboratorsResolver : IValueResolver<EventRequestDto, Event, List<EventCollaboratorsByDate>>
{
    private readonly IMapper _mapper;

    public DateWiseEventCollaboratorsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public List<EventCollaboratorsByDate> Resolve(EventRequestDto source, Event destination, List<EventCollaboratorsByDate> destMember, ResolutionContext context)
    {
        return [new EventCollaboratorsByDate()
                {
                    EventDate = new DateOnly(),
                    EventCollaborators = _mapper.Map<List<EventCollaborator>>(source.EventCollaborators)
                }
               ];
    }
}
