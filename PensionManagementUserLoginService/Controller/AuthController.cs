using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PensionManagementUserLoginService.DTO;
using PensionManagementUserLoginService.ExceptionHandling;
using PensionManagementUserLoginService.Models.Repository.Interfaces;

namespace PensionManagementUserLoginService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                var identityUser = await _userManager.FindByEmailAsync(request.Email);
                if (identityUser == null)
                {
                    throw new loginExceptions("User Not Found.");
                }
                var checkPassportResult = await _userManager.CheckPasswordAsync(identityUser, request.Password);
                if (!checkPassportResult)
                {
                    throw new loginExceptions("Password is incorrect");
                }
                var roles = await _userManager.GetRolesAsync(identityUser);

                var jwtToken = _tokenRepository.CreateJwtToken(identityUser, roles.ToList());
                var response = new LoginRespondDTO()
                {

                    Email = request.Email,
                    Roles = roles.ToList(),
                    Token = jwtToken,
                    Id = identityUser.Id
                };
                return Ok(response);
            }
            catch(loginExceptions ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(404,ModelState);
            }
            
            catch(Exception ex)
            {
                return StatusCode(500, "An unexpected error occured,please try later");
            }
           
            
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = new IdentityUser
            {
                UserName = request.UserName?.Trim(),
                Email = request.Email?.Trim()

            };
            var identityResult = await _userManager.CreateAsync(user, request.Password);
            if (identityResult.Succeeded)
            {
                identityResult = await _userManager.AddToRoleAsync(user, "Reader");
                if (identityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }

                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            return ValidationProblem(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword( [FromBody] LoginRequestDTO loginRequest)
        {
            
            try
            {
                var result = await _userManager.FindByEmailAsync(loginRequest.Email);
                if(result == null)
                {
                    throw new loginExceptions("user Not Found");
                }
                var tokenResult = await _userManager.GeneratePasswordResetTokenAsync(result);
                var res = await _userManager.ResetPasswordAsync(result, tokenResult, loginRequest.Password);
                return Ok(res);
            }
            catch (loginExceptions ex)
            {

                ModelState.AddModelError("", ex.Message);
                return StatusCode(404,ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occured,please try later");
            }
        }
     
    }
}
