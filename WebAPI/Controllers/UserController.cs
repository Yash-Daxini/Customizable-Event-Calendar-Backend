using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.IServices;
using AutoMapper;
using WebAPI.Dtos;
using Core.Exceptions;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.Identity;

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
    [HttpGet("/usersForInvite")]
    public async Task<ActionResult> GetUsersForInvite()
    {
        try
        {
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());
            List<User> users = await _userService.GetUsersForInvite(userId);
            return Ok(_mapper.Map<List<UserResponseDto>>(users));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [Authorize]
    [HttpGet("")]
    public async Task<ActionResult> GetUserById()
    {
        try
        {
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

            User? user = await _userService.GetUserById(userId);

            return Ok(_mapper.Map<UserResponseDto>(user));
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
    public async Task<ActionResult> AddUser([FromBody] UserRequestDto userRequestDto)
    {
        try
        {
            User user = _mapper.Map<User>(userRequestDto);

            var result = await _userService.SignUp(user);
            if (result.Succeeded)
                return Ok(new { message = "Successfully SingUp !" });
            else
                return BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UserRequestDto userDto)
    {
        try
        {
            User user = _mapper.Map<User>(userDto);

            var result = await _userService.UpdateUser(user);

            if (result.Succeeded)
                return Ok(new { message = "Successfully Updated User" });
            else
                return BadRequest(result.Errors);
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
    [HttpDelete("")]
    public async Task<ActionResult> DeleteUser()
    {
        try
        {
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

            var result = await _userService.DeleteUser(userId);

            if (result.Succeeded)
                return Ok(new { message = "Successfully Deleted User" });
            else
                return BadRequest(result.Errors);
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
    public async Task<ActionResult> LogIn([FromBody] AuthenticateRequestDto authenticateRequestDto)
    {
        try
        {
            User user = _mapper.Map<User>(authenticateRequestDto);

            AuthenticateResponse? authenticateResponse = await _userAuthenticationService.LogIn(user);

            AuthenticateResponseDto authenticateResponseDto = _mapper.Map<AuthenticateResponseDto>(authenticateResponse);

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
