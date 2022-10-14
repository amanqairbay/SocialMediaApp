using System;
using Core.Entities;

namespace Core.Services
{
    /// <summary>
    /// Represents the token service.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Creates and returns the user token.
        /// </summary>
        /// <param name="user">The user data.</param>
        /// <returns>
        /// A string containing the user token.
        /// </returns>
        string CreateToken(AppUser user);
    }
}

