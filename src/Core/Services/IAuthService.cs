using Core.DTOs.User;

namespace Core.Services
{
    /// <summary>
    /// Authentication service interface.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Gets and returns the current user.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the user.
        /// </returns>
        Task<UserDto> GetCurrentUserAsync();

        /// <summary>
        /// Checks if the mail exists.
        /// </summary>
        /// <param name="email">The user email to check for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing a boolean result.
        /// </returns>
        Task<bool> CheckEmailExistsAsync(string email);

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="loginDto">The user data to login for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the user.
        /// </returns>
        Task<UserDto> Login(UserToLoginDto loginDto);

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerDto">The user data to register for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the user.
        /// </returns>
        Task<UserDto> Register(UserForRegisterDto registerDto);
    }
}

