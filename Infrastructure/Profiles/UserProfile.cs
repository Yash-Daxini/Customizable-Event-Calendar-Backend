using AutoMapper;
using Core.Domain.Models;
using Infrastructure.DataModels;

namespace Infrastructure.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDataModel, User>();
        CreateMap<User, UserDataModel>();
    }
}
