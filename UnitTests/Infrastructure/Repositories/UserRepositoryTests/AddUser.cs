using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using Infrastructure.Profiles;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class AddUser
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    private readonly List<User> _users;

    public AddUser()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new EventProfile());
            mc.AddProfile(new SharedCalendarProfile());
            mc.AddProfile(new EventCollaboratorProfile());
            mc.AddProfile(new UserProfile());
        });
        IMapper mapper = mappingConfig.CreateMapper();
        _mapper = mapper;
        _users = [ new(){

            Id = 1,
            Name = "a",
            Password = "a",
            Email = "a",
        }];
    }

    [Fact]
    public async Task Should_AddUserAndReturnUserId_When_CallsTheRepositoryMethod()
    {
        _dbContext = await new UserRepositoryDBContext().GetDatabaseContext();

        User user = new()
        {
            Name = "b",
            Password = "b",
            Email = "b",
        };

        UserRepository userRepository = new(_dbContext, _mapper);

        int id = await userRepository.Add(user);

        user.Id = id;

        User? addedUser = await userRepository.GetUserById(id);

        Assert.Equivalent(user, addedUser);
    }
}
