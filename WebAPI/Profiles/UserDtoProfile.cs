using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class UserDtoProfile : Profile
{
    public UserDtoProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }
}
