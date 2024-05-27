using AutoMapper;
using Core.Domain;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class DateWiseParticipantsResolver : IValueResolver<EventRequestDto, Event, List<EventCollaboratorsByDate>>
{
    private readonly IMapper _mapper;

    public DateWiseParticipantsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public List<EventCollaboratorsByDate> Resolve(EventRequestDto source, Event destination, List<EventCollaboratorsByDate> destMember, ResolutionContext context)
    {
        return [new EventCollaboratorsByDate()
                {
                    EventDate = new DateOnly(),
                    EventCollaborators = _mapper.Map<List<EventCollaborator>>(source.Participants)
                }
               ];
    }
}
