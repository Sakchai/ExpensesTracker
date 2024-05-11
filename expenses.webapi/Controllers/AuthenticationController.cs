using Expenses.Core;
using Expenses.Core.CustomExceptions;
using Expenses.DB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Expenses.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthenticationController(IUserService userService,
            IPasswordService passwordService,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userService = userService;
            _passwordService = passwordService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            try
            {
                var result = await _userService.SignUp(user);
                return Created("", result);
            }
            catch (UsernameAlreadyExistsException e)
            {
                return StatusCode(409, e.Message);
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] User user)
        {
            try
            {
                var userToSignIn = await _userManager.FindByEmailAsync(user.UserName);

                if (userToSignIn == null)
                {
                    throw new InvalidUsernamePasswordException("Invalid username or password");
                }
                var verified = _passwordService.VerifyPassword(userToSignIn.Password, user.Password);

                if (verified == PasswordVerificationResult.Success)
                {
                    return Created("", userToSignIn);
                }
                else
                {
                    throw new InvalidUsernamePasswordException("Invalid username or password");
                }

            }
            catch (InvalidUsernamePasswordException e)
            {
                return StatusCode(401, e.Message);
            }
        }

        [HttpPost("google")]
        public async Task<ActionResult> Auth([FromQuery] string token)
        {
            var payload = await ValidateAsync(token, new ValidationSettings
            {
                Audience = new[]
               {
                    Environment.GetEnvironmentVariable("CLIENT_ID")
                }
            });

            var result = await _userService.ExternalSignIn(new User
            {
                Email = payload.Email,
                ExternalId = payload.Subject,
                ExternalType = "GOOGLE"
            });

            return Created("", result);
        }
    }
}
