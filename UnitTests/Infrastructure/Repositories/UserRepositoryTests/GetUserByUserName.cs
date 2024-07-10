using AutoMapper;
using Core.Entities;
using FluentAssertions;
using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Repositories;
using UnitTests.Builders.DataModelBuilder;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class GetUserByUserName : UserRepositorySetup, IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    public DbContextEventCalendar _dbContext;

    public GetUserByUserName(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_Return_User_When_UserAvailableWithName()
    {
        UserDataModel userDataModel = new UserDataModelBuilder()
                                     .WithId(1)
                                     .WithUserName("a")
                                     .WithEmail("abc@gmail.com")
                                     .WithNormalizeUserName("A")
                                     .Build();

        _dbContext = new DatabaseBuilder()
                    .WithUser(userDataModel)
                    .Build();

        SetUpIdentityObjects(_dbContext);

        User expectedResult = new UserBuilder(1)
                             .WithName("a")
                             .WithEmail("abc@gmail.com")
                             .Build();

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        User? actualResult = await userRepository.GetUserByUserName("a");

        actualResult.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_Return_Null_When_UserNotAvailableWithName()
    {
        UserDataModel userDataModel = new UserDataModelBuilder()
                                     .WithId(1)
                                     .WithUserName("a")
                                     .WithEmail("abc@gmail.com")
                                     .WithNormalizeUserName("A")
                                     .Build();

        _dbContext = new DatabaseBuilder()
            .WithUser(userDataModel)
            .Build();

        SetUpIdentityObjects(_dbContext);

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        User? actualResult = await userRepository.GetUserByUserName("b");

        actualResult.Should().BeNull();
    }

}
