using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using FluentAssertions;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class AddUser : IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    private readonly UserManager<UserDataModel> _userManager;
    private readonly SignInManager<UserDataModel> _signInManager;

    private readonly DbContextEventCalendar _dbContext;

    public AddUser(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;

        _dbContext = new UserRepositoryDBContext().GetDatabaseContext();

        var services = new ServiceCollection();

        services.AddSingleton(_dbContext);

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
    public async Task Should_Return_AddedUserId_When_UserIsValid()
    {
        User user = new UserBuilder(2)
                    .WithName("b")
                    .WithPassword("bbBB@1")
                    .WithEmail("b@gmail.com")
                    .Build();

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        var result = await userRepository.SignUp(user);

        result.Succeeded.Should().BeTrue();
    }
}
