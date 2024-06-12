using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class AuthenticateRequestDtoProfile : Profile
{
    public AuthenticateRequestDtoProfile()
    {
        CreateMap<AuthenticateRequestDto, User>();
    }
}
