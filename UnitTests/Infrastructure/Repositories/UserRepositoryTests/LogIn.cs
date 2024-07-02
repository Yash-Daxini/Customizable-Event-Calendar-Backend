using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure.DataModels;
using Microsoft.AspNetCore.Identity;
using FluentAssertions;
using UnitTests.Builders.EntityBuilder;
using UnitTests.Builders.DataModelBuilder;
using Infrastructure;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class LogIn : UserRepositorySetup, IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    private DbContextEventCalendar _dbContext;

    public LogIn(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_Return_SuccessResult_When_UserWithValidCredentials()
    {
        UserDataModel userDataModel = new UserDataModelBuilder()
                                     .WithId(1)
                                     .WithUserName("a")
                                     .WithNormalizeUserName("A")
                                     .WithPasswordHash("AQAAAAIAAYagAAAAEFVo/8EEd6wiXBAHoU2ZdzjgEzJRnLm0PaXPO1q41Ns09QyF/L+BMTafbFxAALlKKg==")
                                     .WithEmail("abc@gmail.com")
                                     .WithSecurityStamp(Guid.NewGuid().ToString())
                                     .Build();

        _dbContext = new DatabaseBuilder()
                    .WithUser(userDataModel)
                    .Build();

        SetUpIndentityObjects(_dbContext);

        User user = new UserBuilder(1)
                    .WithName("a")
                    .WithPassword("aaAA@1")
                    .WithEmail("abc@gmail.com")
                    .Build();

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        SignInResult authResult = await userRepository.LogIn(user);

        authResult.Should().Be(SignInResult.Success);
    }

    [Fact]
    public async Task Should_ReturnFailedResult_When_UserWithInValidCredentials()
    {
        _dbContext = new DatabaseBuilder()
                    .Build();

        SetUpIndentityObjects(_dbContext);

        User user = new UserBuilder(5)
                    .WithName("a")
                    .WithPassword("b")
                    .WithEmail("b")
                    .Build();

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        SignInResult authResponse = await userRepository.LogIn(user);

        authResponse.Should().Be(SignInResult.Failed);
    }
}
