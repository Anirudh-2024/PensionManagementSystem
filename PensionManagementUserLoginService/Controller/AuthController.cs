using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PensionManagementUserLoginService.DTO;
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
            var identityUser = await _userManager.FindByEmailAsync(request.Email);
           
            if (identityUser is not null)
            {
               var checkPassportResult = await _userManager.CheckPasswordAsync(identityUser, request.Password);
                
                if (checkPassportResult)
                {
                    var roles = await _userManager.GetRolesAsync(identityUser);

                    var jwtToken=_tokenRepository.CreateJwtToken(identityUser,roles.ToList());
                    var response = new LoginRespondDTO()
                    {
                        
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = jwtToken,
                        Id = identityUser.Id
                    };
                    return Ok(response);
                }


            }
            ModelState.AddModelError("", "Email or passward is Incorrect");
            return ValidationProblem(ModelState);
            
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
            var result = await _userManager.FindByEmailAsync(loginRequest.Email);
            var tokenResult = await _userManager.GeneratePasswordResetTokenAsync(result);
            if(result != null)
            {
                var res = await _userManager.ResetPasswordAsync(result, tokenResult, loginRequest.Password);
                return Ok(res);
            }
            ModelState.AddModelError("", "Incorrect Email");
            return ValidationProblem(ModelState);
        }
     
    }
}
