using AutoMapper;
using Core.Domain;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class EventResponseDtoProfile : Profile
{
    public EventResponseDtoProfile()
    {
        CreateMap<Event, EventResponseDto>();
        CreateMap<EventResponseDto, Event>();
    }
}
