using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class GetUserById : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    public GetUserById(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_ReturnUser_When_UserWithIdAvailable()
    {
        _dbContext = await new UserRepositoryDBContext().GetDatabaseContext();

        User user = new()
        {

            Id = 1,
            Name = "a",
            Password = "a",
            Email = "a",
        };

        UserRepository userRepository = new(_dbContext, _mapper);

        User? userById = await userRepository.GetUserById(1);

        Assert.Equivalent(user,userById);
    }
}
