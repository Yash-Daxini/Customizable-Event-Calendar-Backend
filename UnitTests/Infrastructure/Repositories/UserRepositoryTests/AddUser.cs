using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Infrastructure.DataModels;
using Microsoft.AspNetCore.Identity;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class AddUser : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    private readonly IConfiguration _configuration;
    private readonly UserManager<UserDataModel> _userManager;
    private readonly SignInManager<UserDataModel> _signInManager;

    public AddUser(AutoMapperFixture autoMapperFixture,UserManager<UserDataModel> userManager,SignInManager<UserDataModel> signInManager)
    {
        _mapper = autoMapperFixture.Mapper;
        _configuration = Substitute.For<IConfiguration>();
        _userManager = userManager;
        _signInManager = signInManager;
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

        UserDataModel userDataModel = new()
        {
            UserName = "b",
            Email = "b",
        };

        UserRepository userRepository = new(_dbContext, _mapper, _configuration, _userManager, _signInManager);

        _userManager.CreateAsync(userDataModel).ReturnsForAnyArgs(IdentityResult.Success);

        var result = await userRepository.SignUp(user);

        Assert.True(result.Succeeded);
    }
}
