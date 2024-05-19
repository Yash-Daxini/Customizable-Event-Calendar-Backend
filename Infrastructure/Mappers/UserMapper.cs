using Infrastructure.DataModels;
using Infrastructure.DomainEntities;

namespace Infrastructure.Mappers;

public class UserMapper
{
    public UserModel MapUserEntityToModel(User user)
    {
        return new UserModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
        };
    }

    public User MapUserModelToEntity(UserModel user)
    {
        return new User
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
        };
    }
}
