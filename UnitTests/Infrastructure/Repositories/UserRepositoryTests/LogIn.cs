using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using NSubstitute;
using Infrastructure.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using FluentAssertions;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class LogIn : IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    private readonly UserManager<UserDataModel> _userManager;
    private readonly SignInManager<UserDataModel> _signInManager;

    public LogIn(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;

        var dbContext = new UserRepositoryDBContext().GetDatabaseContext();

        var services = new ServiceCollection();

        services.AddSingleton(dbContext);

        services.AddIdentity<UserDataModel, IdentityRole<int>>()
            .AddEntityFrameworkStores<DbContextEventCalendar>()
            .AddDefaultTokenProviders();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddLogging();

        var serviceProvider = services.BuildServiceProvider();

        _userManager = serviceProvider.GetRequiredService<UserManager<UserDataModel>>();

        _signInManager = serviceProvider.GetRequiredService<SignInManager<UserDataModel>>();
    }

    [Fact]
    public async Task Should_ReturnSuccessResult_When_UserWithValidCredentials()
    {
        User user = new()
        {
            Id = 1,
            Name = "A",
            Password = "aaAA@1",
            Email = "abc@gmail.com",
        };

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        SignInResult authResult = await userRepository.LogIn(user);

        authResult.Should().Be(SignInResult.Success);
    }

    [Fact]
    public async Task Should_ReturnFailedResult_When_UserWithInValidCredentials()
    {
        User user = new()
        {
            Id = 5,
            Name = "b",
            Password = "b",
            Email = "b",
        };

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        SignInResult authResponse = await userRepository.LogIn(user);

        authResponse.Should().Be(SignInResult.Failed);
    }
}
