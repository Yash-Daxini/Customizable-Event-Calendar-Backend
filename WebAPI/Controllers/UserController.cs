using Infrastructure.DomainEntities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userRepository.GetAllUsers());
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUserById([FromRoute] int userId)
        {
            UserModel? user = await _userRepository.GetUserById(userId);

            if (user is null) return NotFound();

            return Ok(user);
        }

        [HttpPost("")]
        public async Task<ActionResult> AddUser([FromBody] UserModel userModel)
        {
            int addedUserId = await _userRepository.AddUser(userModel);
            return CreatedAtAction(nameof(GetUserById), new { userId = addedUserId, controller = "user" }, addedUserId);
        }

        [HttpPut("{bookId}")]
        public async Task<ActionResult> UpdateUser([FromRoute] int userId, [FromBody] UserModel userModel)
        {
            int addedUserId = await _userRepository.UpdateUser(userId, userModel);
            return CreatedAtAction(nameof(GetUserById), new { userId = addedUserId, controller = "user" }, addedUserId);
        }

        [HttpDelete("{bookId}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int userId)
        {
            await _userRepository.DeleteUser(userId);
            return Ok();
        }

    }
}
