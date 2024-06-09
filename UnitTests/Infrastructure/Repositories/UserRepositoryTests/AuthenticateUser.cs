using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class AuthenticateUser : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    public AuthenticateUser(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_ReturnUser_When_UserWithValidCredentials()
    {
        _dbContext = await new UserRepositoryDBContext().GetDatabaseContext();

        User user = new()
        {

            Id = 1,
            Name = "a",
            Password = "a",
            Email = "a",
        };

        UserRepository userRepository = new(_dbContext, _mapper);

        User? authenticatedUser = await userRepository.AuthenticateUser(user);

        Assert.Equivalent(user, authenticatedUser);
    }
}
