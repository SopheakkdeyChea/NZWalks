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

        public AuthenticatesController(IUserRepository userRepository, ITokenHandler tokenHandler) 
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("LoginAsync")]
        public async Task<IActionResult> LoginAsync(UserLoginRequest userLoginRequest)
        {
            // Validate the incoming request by using fluent validator

            // Check if user is authenticated
            // Check Username and password
            var user = await _userRepository.AuthenticateAsync
                (userLoginRequest.UserName, userLoginRequest.Password);

            if (user != null)
            {
                // Generate a JWT Token
                var token = await _tokenHandler.CreateTokenAsync(user);
                return Ok(new { Token  = token });
            }

            return BadRequest(new {message = "Username or Password is incorrect." });
        }
    }
}
