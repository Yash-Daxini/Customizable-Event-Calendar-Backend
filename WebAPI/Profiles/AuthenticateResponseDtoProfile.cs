﻿using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class AuthenticateResponseDtoProfile : Profile
{
    public AuthenticateResponseDtoProfile()
    {
        CreateMap<AuthenticateResponse, AuthenticateResponseDto>();
    }
}