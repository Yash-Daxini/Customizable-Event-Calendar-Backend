using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using Infrastructure.Profiles;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class AuthenticateUser
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    private readonly List<User> _users;

    public AuthenticateUser()
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
    public async Task Should_ReturnUser_When_UserWithValidCredentials()
    {
        _dbContext = await new UserRepositoryDBContext().GetDatabaseContext();

        UserRepository userRepository = new(_dbContext, _mapper);

        User? user = await userRepository.AuthenticateUser(_users[0]);

        Assert.Equivalent(_users[0], user);
    }
}
