using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.IServices;
using AutoMapper;
using WebAPI.Dtos;
using Core.Exceptions;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IUserAuthenticationService userAuthenticationService, IMapper mapper)
    {
        _userService = userService;
        _userAuthenticationService = userAuthenticationService;
        _mapper = mapper;
    }


    [Authorize]
    [HttpGet("{userId}")]
    public async Task<ActionResult> GetUserById([FromRoute] int userId)
    {
        try
        {
            User? user = _userService.GetUserById(userId).Result;

            if (user is null) return NotFound();

            return Ok(_mapper.Map<UserDto>(user));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpPost("")]
    public async Task<ActionResult> AddUser([FromBody] UserDto userDto)
    {
        try
        {
            User user = _mapper.Map<User>(userDto);
            int addedUserId = await _userService.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = addedUserId, controller = "user" }, new { addedUserId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UserDto userDto)
    {
        try
        {
            User user = _mapper.Map<User>(userDto);
            await _userService.UpdateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.Id, controller = "user" }, new { user.Id });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [Authorize]
    [HttpDelete("{userId}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int userId)
    {
        try
        {
            await _userService.DeleteUser(userId);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpPost("~/api/auth/login")]
    public async Task<ActionResult> AuthenticateUser([FromBody] User user)
    {
        try
        {
            AuthenticateResponseDto authenticateResponseDto = _mapper.Map<AuthenticateResponseDto>(await _userAuthenticationService.Authenticate(user));

            return Ok(authenticateResponseDto);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (AuthenticationFailedException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
