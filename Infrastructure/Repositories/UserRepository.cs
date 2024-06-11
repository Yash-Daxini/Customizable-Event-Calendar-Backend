using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Core.Interfaces.IRepositories;
using Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User, UserDataModel>, IUserRepository
{
    private readonly DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    private readonly IConfiguration _configuration;

    public UserRepository(DbContextEventCalendar dbContextEvent, IMapper mapper, IConfiguration configuration) : base(dbContextEvent, mapper)
    {
        _dbContext = dbContextEvent;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<User?> GetUserById(int userId)
    {
        return _mapper.Map<User>(await _dbContext.Users
                               .Where(user => user.Id == userId)
                               .FirstOrDefaultAsync());
    }

    public async Task<AuthenticateResponse?> AuthenticateUser(User user) //Extra
    {
        UserDataModel? userDataModel = await _dbContext
                                      .Users
                                      .FirstOrDefaultAsync(userObj => userObj.Name == user.Name
                                                                   && userObj.Password == user.Password);

        User validatedUser = _mapper.Map<User>(userDataModel);

        var token = "";

        if (validatedUser is not null)
        {
            token = await GenerateJwtToken(validatedUser);
        }

        return new AuthenticateResponse(validatedUser, token);
    }

    private async Task<string> GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() =>
        {

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        });

        return tokenHandler.WriteToken(token);
    }
}
