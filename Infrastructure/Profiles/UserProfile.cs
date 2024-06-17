using AutoMapper;
using Core.Entities;
using Infrastructure.DataModels;

namespace Infrastructure.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDataModel, User>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));
        CreateMap<User, UserDataModel>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name));
    }
}
