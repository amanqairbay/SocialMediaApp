using Core.DTOs.Photo;
using Core.Errors;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    public class PhotosController : BaseApiController
    {
        private readonly IPhotoService _photoService;
        private readonly IUserService _userService;

        public PhotosController(
            IPhotoService photoService,
            IUserService userService)
        {
            _photoService = photoService;
            _userService = userService;
        }

        /// <summary>
        /// Gets and returns a photo, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The photo identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the photo matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        /// <response code="200">If the photo exists.</response>
        /// <response code="404">If the photo doesn't exist.</response>
        [HttpGet("/{id}", Name = "GetPhoto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPhoto(long id)
        {
            var photo = await _photoService.GetPhotoByIdAsync(id);

            return Ok(photo);
        }

        /// <summary>
        /// Adds a new photo for the user.
        /// </summary>
        /// <param name="userId">The user identifier to get for.</param>
        /// <param name="photoForCreationDto">The photo to return for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing a photo and the result of adding it.
        /// </returns>
        /// <response code="201">If the photo successfully added.</response>
        /// <response code="400">If the photo could not be added.</response>
        /// <response code="401">If the user is not logged in.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddPhotoForUser(long userId, [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            var (photoId, photoToReturn) = await _photoService.AddPhotoForUser(userId, photoForCreationDto);
            
            return CreatedAtRoute("GetPhoto", new { id = photoId }, photoToReturn); 
        }

        /// <summary>
        /// Sets the user's main photo. 
        /// </summary>
        /// <param name="userId">The user identifier to get for.</param>
        /// <param name="id">The photo identifier to set for.</param>
        /// <returns>
        /// A task that represents an asynchronous operation containing the result of setting the main photo.
        /// </returns>
        /// <response code="204">If the photo is successfully installed as the main one.</response>
        /// <response code="400">If the photo is already set as the main one or cannot be set.</response>
        /// <response code="401">If the user is not logged in.</response>
        [HttpPost("{id}/setMain")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SetMainPhoto(long userId, long id)
        {
            await _photoService.SetMainPhoto(userId, id);

            return NoContent();
        }

        /// <summary>
        /// Deletes the photo, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="userId">The user identifier to get for.</param>
        /// <param name="id">The photo identifier to get for.</param>
        /// <returns>
        /// A task that represents an asynchronous operation containing the result of deleting the user photo.
        /// </returns>
        /// <response code="20қ">If the photo is successfully deleted.</response>
        /// <response code="400">If the photo is the main one and cannot be deleted.</response>
        /// <response code="401">If the user is not logged in.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeletePhoto(long userId, long id)
        {
            await _photoService.DeletePhoto(userId, id);

            return Ok();
        }
    }
}

