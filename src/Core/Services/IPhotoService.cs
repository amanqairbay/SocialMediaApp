using Core.DTOs.Photo;
using Core.Entities;

namespace Core.Services
{
    /// <summary>
    /// Represents the photo service interface.
    /// </summary>
    public interface IPhotoService
    {
        /// <summary>
        /// Gets and returns a photo, if any, who has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The photo identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the photo matching the specified <paramref name="photoId" /> if it exists.
        /// </returns>
        Task<PhotoForReturnDto> GetPhotoByIdAsync(long id);

        /// <summary>
        /// Adds the user photo.
        /// </summary>
        /// <param name="userId">The user identifier to get for.</param>
        /// <param name="photoForCreationDto">The photo to return for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing a photo and the result of adding it.
        /// </returns>
        Task<(long photoId, PhotoForReturnDto photoForReturnDto)> AddPhotoForUser(long userId, PhotoForCreationDto photoForCreationDto);

        /// <summary>
        /// Sets the user's main photo. 
        /// </summary>
        /// <param name="userId">The user identifier to get for.</param>
        /// <param name="id">The photo identifier to set for.</param>
        /// <returns>
        /// A task that represents an asynchronous operation containing the result of setting the main photo.
        /// </returns>
        Task SetMainPhoto(long userId, long id);

        /// <summary>
        /// Deletes the photo, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="userId">The user identifier to get for.</param>
        /// <param name="id">The photo identifier to get for.</param>
        /// <returns>
        /// A task that represents an asynchronous operation containing the result of deleting the user photo.
        /// </returns>
        Task DeletePhoto(long userId, long id);
    }
}

