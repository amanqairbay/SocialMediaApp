using Core.DTOs.User;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Gets and returns the current user.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the user.
        /// </returns>
        /// <response code="200">If the current user is received.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _authService.GetCurrentUserAsync();

            return Ok(user);
        }

        /// <summary>
        /// Checks if the mail exists.
        /// </summary>
        /// <param name="email">The user email to check for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing a boolean result.
        /// </returns>
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _authService.CheckEmailExistsAsync(email);
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="loginDto">The user data to login for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the user.
        /// </returns>
        /// <response code="200">If the user is logged in.</response>
        /// <response code="401">If the user is not logged in.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserToLoginDto loginDto)
        {
            var user = await _authService.Login(loginDto);

            return Ok(user);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerDto">The user dat to register for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the user.
        /// </returns>
        /// <response code="200">If registration is successful.</response>
        /// <response code="400">If the user mail address is in use or invalid syntax for this request.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto registerDto)
        {
            var user = await _authService.Register(registerDto);

            return Ok(user);
        }
    }
}

