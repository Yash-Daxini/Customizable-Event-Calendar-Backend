using Infrastructure.DataModels;
using Core.Domain;

namespace Infrastructure.Mappers;

public class UserMapper
{
    public UserModel MapUserEntityToModel(UserDataModel user)
    {
        return new UserModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
        };
    }

    public UserDataModel MapUserModelToEntity(UserModel user)
    {
        return new UserDataModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
        };
    }
}
