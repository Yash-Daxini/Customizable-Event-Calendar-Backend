using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class UserRequestDtoProfile : Profile
{
    public UserRequestDtoProfile()
    {
        CreateMap<UserRequestDto, User>();
    }
}
