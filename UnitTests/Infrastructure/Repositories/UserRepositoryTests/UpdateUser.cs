using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using Infrastructure.Profiles;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class UpdateUser
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    private readonly List<User> _users;

    public UpdateUser()
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
    public async Task Should_UpdateUser_When_UserWithIdAvailable()
    {
        _dbContext = await new UserRepositoryDBContext().GetDatabaseContext();

        User user = new()
        {
            Id = 1,
            Name = "b",
            Password = "b",
            Email = "b",
        };

        _dbContext.ChangeTracker.Clear();

        UserRepository userRepository = new(_dbContext, _mapper);

        await userRepository.Update(user);

        User? updatedUser = await userRepository.GetUserById(1);

        Assert.Equivalent(user, updatedUser);
    }
}
