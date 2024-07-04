using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class UserResponseDtoProfile : Profile
{
    public UserResponseDtoProfile()
    {
        CreateMap<User, UserResponseDto>();
        CreateMap<UserResponseDto, User>();
    }
}
