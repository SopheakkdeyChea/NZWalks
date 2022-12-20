using LoggerService.Repositories;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO.Users;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthenticatesController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;
        private readonly ILoggerRepository _logger;

        public AuthenticatesController(IUserRepository userRepository, ITokenHandler tokenHandler,
                                       ILoggerRepository loggerRepository) 
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
            _logger = loggerRepository;
        }

        [HttpPost]
        [Route("LoginAsync")]
        public async Task<IActionResult> LoginAsync(UserLoginRequest userLoginRequest)
        {
            var myMethodName = ControllerContext.ActionDescriptor.AttributeRouteInfo.Template.ToString();

            // Validate the incoming request by using fluent validator
            try
            {
                // Check if user is authenticated and Check Username and password
                var user = await _userRepository.AuthenticateAsync
                    (userLoginRequest.UserName, userLoginRequest.Password);

                if (user != null)
                {
                    // Generate a JWT Token
                    var token = await _tokenHandler.CreateTokenAsync(user);
                    _logger.LogInfo($"{myMethodName} |Token: {token}");
                    return Ok(new { Token = token });
                }

                _logger.LogWarning($"{myMethodName} | Username or Password is incorrect.");
                return BadRequest(new { message = "Username or Password is incorrect." });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{myMethodName} | Failed to login {ex.Message}");
                return BadRequest($"Failed to login {ex.Message}");
            }
        }
    }
}
