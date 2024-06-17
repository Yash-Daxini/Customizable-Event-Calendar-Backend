using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Infrastructure.DataModels;
using Microsoft.AspNetCore.Identity;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class LogIn : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    private readonly IConfiguration _configuration;

    private readonly UserManager<UserDataModel> _userManager;
    private readonly SignInManager<UserDataModel> _signInManager;

    public LogIn(AutoMapperFixture autoMapperFixture, UserManager<UserDataModel> userManager, SignInManager<UserDataModel> signInManager)
    {
        _mapper = autoMapperFixture.Mapper;
        _configuration = Substitute.For<IConfiguration>();
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [Fact]
    public async Task Should_ReturnAuthenticationResponse_When_UserWithValidCredentials()
    {
        _dbContext = await new UserRepositoryDBContext().GetDatabaseContext();

        User user = new()
        {
            Id = 1,
            Name = "a",
            Password = "a",
            Email = "a",
        };

        UserRepository userRepository = new(_dbContext, _mapper, _configuration, _userManager, _signInManager);

        _configuration["JWT:Secret"].Returns("ADSJIDJFIEKNFIOJVNBOIEDFEW90432jkj");

        AuthenticateResponse? authenticatedUser = await userRepository.LogIn(user);

        Assert.Equivalent(user.Name, authenticatedUser.Name);
    }

    [Fact]
    public async Task Should_ReturnNull_When_UserWithInValidCredentials()
    {
        _dbContext = await new UserRepositoryDBContext().GetDatabaseContext();

        User user = new()
        {
            Id = 5,
            Name = "b",
            Password = "b",
            Email = "b",
        };

        UserRepository userRepository = new(_dbContext, _mapper, _configuration, _userManager, _signInManager);

        _configuration["JWT:Secret"].Returns("ADSJIDJFIEKNFIOJVNBOIEDFEW90432jkj");

        AuthenticateResponse? authenticatedUser = await userRepository.LogIn(user);

        Assert.Null(authenticatedUser);
    }
}
