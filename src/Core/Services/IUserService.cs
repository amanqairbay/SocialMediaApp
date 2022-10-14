using Core.DTOs.User;
using Core.RequestFeatures;
using Microsoft.AspNetCore.Identity;

namespace Core.Services
{
    /// <summary>
    /// Represents the user service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets and returns a list of users by parameters.
        /// </summary>
        /// <param name="userParams">The user params to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of users.
        /// </returns>
        Task<(IEnumerable<UserForListDto> users, MetaData metaData)> GetPagedListUsersAsync(UserParameters userParams);

        /// <summary>
        /// Gets and returns a user, if any, who has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The user identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the user matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        Task<UserForDetailedDto> GetUserByIdAsync(long id);

        /// <summary>
        /// Checks a user, if any, who has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The user identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation checking whether the user exists.
        /// </returns>
        Task CheckIfUserExists(long id);

        /// <summary>
        /// Updates the user
        /// </summary>
        /// <param name="id">The user identifier to get for.</param>
        /// <param name="userForUpdateDto">The user data to update for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the result of the user update.
        /// </returns>
        Task UpdateUser(long id, UserForUpdateDto userForUpdateDto);

        /// <summary>
        /// Likes the user.
        /// </summary>
        /// <param name="id">The user identifier to get for.</param>
        /// <param name="recipientId">The recipient identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the result of the user's like.
        /// </returns>
        Task LikeUser(long id, long recipientId);
    }
}

