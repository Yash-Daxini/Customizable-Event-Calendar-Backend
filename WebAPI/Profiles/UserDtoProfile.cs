using AutoMapper;
using Core.Domain;
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
