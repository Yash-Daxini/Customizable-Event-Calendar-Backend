using Core.Entities;

namespace UnitTests.Builders;

public class UserBuilder
{
    private readonly User _user = new();

    public UserBuilder(int id)
    {
        _user.Id = id;
    }

    public UserBuilder WithId(int id)
    {
        _user.Id = id;
        return this;
    }

    public UserBuilder WithName(string name)
    {
        _user.Name = name;
        return this;
    }

    public UserBuilder WithEmail(string email)
    {
        _user.Email = email;
        return this;
    }

    public UserBuilder WithPassword(string password)
    {
        _user.Password = password;
        return this;
    }

    public User Build() => _user;
}
