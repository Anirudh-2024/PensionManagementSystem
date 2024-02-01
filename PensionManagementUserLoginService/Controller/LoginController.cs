using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionManagementUserLoginService.Models;
using PensionManagementUserLoginService.Models.Repository.Interfaces;

namespace PensionManagementUserLoginService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDetails>> GetUserById(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserById(userId);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<UserDetails>> AddUser([FromBody] UserDetails userDetails)
        {
            try
            {
                var addedUser = await _userRepository.AddUser(userDetails);
                return CreatedAtAction(nameof(GetUserById), new { userId = addedUser.UserId }, addedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{userId}")]
        public async Task<ActionResult<UserDetails>> UpdateUserById(int userId, [FromBody] UserDetails userDetails)
        {
            try
            {
                var updatedUser = await _userRepository.UpdateUserById(userId, userDetails);
                if (updatedUser == null)
                {
                    return NotFound();
                }
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{userId}")]
        public IActionResult DeleteUserById(int userId)
        {
            try
            {
                _userRepository.DeleteUserById(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
