using Core.Interfaces;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUserById([FromRoute] int userId)
        {
            User? user = _userService.GetUserById(userId).Result;

            if (user is null) return NotFound();

            return Ok(user);
        }

        [HttpPost("")]
        public async Task<ActionResult> AddUser([FromBody] User userModel)
        {
            int addedUserId = await _userService.AddUser(userModel);
            return CreatedAtAction(nameof(GetUserById), new { userId = addedUserId, controller = "user" }, addedUserId);
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUser([FromRoute] int userId, [FromBody] User userModel)
        {
            int addedUserId = await _userService.UpdateUser(userId, userModel);
            return CreatedAtAction(nameof(GetUserById), new { userId = addedUserId, controller = "user" }, addedUserId);
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int userId)
        {
            await _userService.DeleteUser(userId);
            return Ok();
        }

    }
}
