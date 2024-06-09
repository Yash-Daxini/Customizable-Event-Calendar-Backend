using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class AddUser : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    public AddUser(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
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

        UserRepository userRepository = new(_dbContext, _mapper);

        int id = await userRepository.Add(user);

        user.Id = id;

        User? addedUser = await userRepository.GetUserById(id);

        Assert.Equivalent(user, addedUser);
    }
}
