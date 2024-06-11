using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class AuthenticateUser : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    private readonly IConfiguration _configuration;

    public AuthenticateUser(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _configuration = Substitute.For<IConfiguration>();
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

        UserRepository userRepository = new(_dbContext, _mapper, _configuration);

        AuthenticateResponse? authenticatedUser = await userRepository.AuthenticateUser(user);

        Assert.Equivalent(user.Name, authenticatedUser.Name);
    }
}
