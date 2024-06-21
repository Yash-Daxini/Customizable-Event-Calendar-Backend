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

public class GetUserById : IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    private readonly UserManager<UserDataModel> _userManager;
    private readonly SignInManager<UserDataModel> _signInManager;

    public GetUserById(AutoMapperFixture autoMapperFixture)
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
    public async Task Should_ReturnUser_When_UserWithIdAvailable()
    {
        User expectedResult = new()
        {

            Id = 1,
            Name = "a",
            Email = "abc@gmail.com",
        };

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        User? actualResult = await userRepository.GetUserById(1);

        actualResult.Should().BeEquivalentTo(expectedResult);
    }
}
