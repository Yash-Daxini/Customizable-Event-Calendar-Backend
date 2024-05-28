using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.IServices;
using AutoMapper;
using WebAPI.Dtos;
using Core.Exceptions;

namespace WebAPI.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                List<User> users = await _userService.GetAllUsers();
                return Ok(_mapper.Map<List<UserDto>>(users));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<ActionResult> AddUser([FromBody] UserDto userDto)
        {
            try
            {
                User user = _mapper.Map<User>(userDto);
                int addedUserId = await _userService.AddUser(user);
                return CreatedAtAction(nameof(GetUserById), new { userId = addedUserId, controller = "user" }, addedUserId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUser([FromRoute] int userId, [FromBody] UserDto userDto)
        {
            try
            {
                User user = _mapper.Map<User>(userDto);
                user.Id = userId;
                int addedUserId = await _userService.UpdateUser(user);
                return CreatedAtAction(nameof(GetUserById), new { userId = addedUserId, controller = "user" }, addedUserId);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("~/api/auth/login")]
        public async Task<ActionResult> AuthenticateUser([FromBody] User user)
        {
            try
            {
                await _userAuthenticationService.Authenticate(user);

                return Ok(new { message = "Login successfully" });
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
}
