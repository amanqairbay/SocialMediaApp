using Core.DTOs.User;
using Core.Errors;
using Core.RequestFeatures;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.Extensions;
using Web.API.Helpers;

namespace Web.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets and returns a list of users by parameters.
        /// </summary>
        /// <param name="userParams">The user params to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of users.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetPgedListUsers([FromQuery]UserParameters userParams)
        {
            var (users, metaData) = await _userService.GetPagedListUsersAsync(userParams);

            Response.AddPagination(metaData.CurrentPage, metaData.PageSize, metaData.TotalCount, metaData.TotalPages);

            return Ok(users);
        }

        /// <summary>
        /// Gets and returns a user, if any, who has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The user identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the user matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        /// <response code="200">If the user exists.</response>
        /// <response code="404">If the user doesn't exist.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(long id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            return Ok(user);
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="id">The user identifier to get for.</param>
        /// <param name="userForUpdateDto">The user to update for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the result of the user update.
        /// </returns>
        /// <response code="204">if the user is successfully updated.</response>
        /// <response code="400">If the user update could not be saved.</response>
        /// <response code="401">If the user is not logged in.</response>
        /// <response code="404">If the user doesn't exist.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, UserForUpdateDto userForUpdateDto)
        {
            await _userService.UpdateUser(id, userForUpdateDto);

            return NoContent();
        }

        /// <summary>
        /// Likes the user.
        /// </summary>
        /// <param name="id">The user identifier to get for.</param>
        /// <param name="recipientId">The recipient identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the result of the user's like.
        /// </returns>
        /// <response code="200">If the user like is successfull.</response>
        /// <response code="400">If the user already like or failed to like.</response>
        /// <response code="401">If the user is not logged in.</response>
        /// <response code="404">If the user (recipient) doesn't exist.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("{id}/like/{recipientId}")]
        public async Task<IActionResult> LikeUser(long id, long recipientId)
        {
            await _userService.LikeUser(id, recipientId);

            return Ok();
        }
    }
}

