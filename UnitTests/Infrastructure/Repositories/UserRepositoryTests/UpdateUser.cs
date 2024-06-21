using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using NSubstitute;
using Microsoft.AspNetCore.Identity;
using Infrastructure.DataModels;
using Microsoft.AspNetCore.Http;
using NSubstitute.ReturnsExtensions;
using FluentAssertions;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class UpdateUser : IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    private readonly UserManager<UserDataModel> _userManager;
    private readonly SignInManager<UserDataModel> _signInManager;

    public UpdateUser(AutoMapperFixture autoMapperFixture)
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
    public async Task Should_UpdateUserAndReturnSuccess_When_UserWithIdAvailable()
    {
        User expectedResult = new()
        {
            Id = 1,
            Name = "a",
            Email = "a",
        };

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        await userRepository.Update(expectedResult);

        User? updatedUser = await userRepository.GetUserById(1);

        updatedUser.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_ReturnFailed_When_UserWithIdNotAvailable()
    {
        User user = new()
        {
            Id = 2,
            Name = "b",
            Email = "b",
        };

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        await userRepository.Update(user);

        User? updatedUser = await userRepository.GetUserById(2);

        updatedUser.Should().BeNull();
    }
}
