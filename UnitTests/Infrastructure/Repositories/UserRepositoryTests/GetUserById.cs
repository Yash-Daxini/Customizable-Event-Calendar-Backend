using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure.DataModels;
using FluentAssertions;
using Infrastructure;
using UnitTests.Builders.EntityBuilder;
using UnitTests.Builders.DataModelBuilder;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class GetUserById : UserRepositorySetup, IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    public DbContextEventCalendar _dbContext;

    public GetUserById(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_Return_User_When_UserAvailable()
    {
        UserDataModel userDataModel = new UserDataModelBuilder()
                                     .WithId(1)
                                     .WithUserName("a")
                                     .WithEmail("abc@gmail.com")
                                     .Build();

        _dbContext = new DatabaseBuilder()
            .WithUser(userDataModel)
            .Build();

        SetUpIndentityObjects(_dbContext);

        User expectedResult = new UserBuilder(1)
                             .WithName("a")
                             .WithEmail("abc@gmail.com")
                             .Build();

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        User? actualResult = await userRepository.GetUserById(1);

        actualResult.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_Return_Null_When_UserUnAvailable()
    {
        _dbContext = new DatabaseBuilder().Build();

        SetUpIndentityObjects(_dbContext);

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        User? actualResult = await userRepository.GetUserById(1);

        actualResult.Should().BeNull();
    }
}
