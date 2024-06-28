using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Infrastructure.DataModels;
using Microsoft.AspNetCore.Http;
using FluentAssertions;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.Builders.EntityBuilder;
using UnitTests.Builders.DataModelBuilder;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class UpdateUser : IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    private readonly UserManager<UserDataModel> _userManager;
    private readonly SignInManager<UserDataModel> _signInManager;

    private readonly DbContextEventCalendar _dbContext;

    public UpdateUser(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;

        UserDataModel userDataModel = new UserDataModelBuilder()
                             .WithId(1)
                             .WithUserName("a")
                             .WithEmail("abc@gmail.com")
                             .WithSecurityStamp(Guid.NewGuid().ToString())
                             .Build();

        _dbContext = new DatabaseBuilder().WithUser(userDataModel).Build();

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
    public async Task Should_Return_Success_When_UserAvailableWithId()
    {
        User expectedResult = new UserBuilder(1)
                              .WithName("a")
                              .WithEmail("abc@gmail.com")
                              .Build();

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        await userRepository.Update(expectedResult);

        User? updatedUser = await userRepository.GetUserById(1);

        updatedUser.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_Return_Failed_When_UserWithIdNotAvailable()
    {
        User user = new UserBuilder(2)
                    .WithName("b")
                    .WithEmail("b")
                    .Build();

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        await userRepository.Update(user);

        User? updatedUser = await userRepository.GetUserById(2);

        updatedUser.Should().BeNull();
    }
}
