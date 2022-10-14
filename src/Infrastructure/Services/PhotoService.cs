using System.Security.Claims;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.DTOs.Photo;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Services;
using Infrastructure.Extensions;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    /// <summary>
    /// Photo service class.
    /// </summary>
    public class PhotoService : IPhotoService
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly IRepository<Photo> _photoRepository;
        private readonly IPhotoRepository _repository;
        private readonly IMapper _mapper;
        private Cloudinary _cloudinary;
        #endregion Fields

        #region Constructor
        public PhotoService(
            IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager,
            IOptions<CloudinarySettings> cloudinaryConfig,
            IRepository<Photo> photoRepository,
            IPhotoRepository repository,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _photoRepository = photoRepository;
            _repository = repository;
            _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;
            
            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion Constructor

        /// <summary>
        /// Gets and returns a photo, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The photo identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the photo matching the specified <paramref name="photoId" /> if it exists.
        /// </returns>
        /// <exception cref="NotFoundException">Thrown if the photo with <paramref name="id" /> does not exist in the database.</exception>
        public async Task<PhotoForReturnDto> GetPhotoByIdAsync(long id)
        {
            var photoFromDb = await _photoRepository.GetByIdAsync(id);

            if (photoFromDb is null)
                throw new NotFoundException($"Photo with {id} doesn't exist in the database.");

            var photoDto = _mapper.Map<PhotoForReturnDto>(photoFromDb);

            return photoDto;
        }

        /// <summary>
        /// Adds a new photo for the user.
        /// </summary>
        /// <param name="userId">The user identifier to get for.</param>
        /// <param name="photoForCreationDto">The photo to return for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing a photo and the result of adding it.
        /// </returns>
        [Obsolete]
        public async Task<(long photoId, PhotoForReturnDto photoForReturnDto)>
            AddPhotoForUser(long userId, PhotoForCreationDto photoForCreationDto)
        {
            if (userId != long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                throw new UnauthorizedException("You are not authorized.");

            var userFromDb = await _userManager.GetUserByIdAsync(userId);

            var file = photoForCreationDto.File;
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);
            var photoForReturnDto = _mapper.Map<PhotoForReturnDto>(photo);

            if (!userFromDb!.Photos.Any(u => u.IsMain)) { photo.IsMain = true; }

            userFromDb.Photos.Add(photo);

            if (await _photoRepository.SaveAll() is not true)
                throw new BadRequestException("Could not add the photo.");

            return (photoId: photo.Id, photoForReturnDto: photoForReturnDto);
        }

        /// <summary>
        /// Sets the user's main photo. 
        /// </summary>
        /// <param name="userId">The user identifier to get for.</param>
        /// <param name="id">The photo identifier to set for.</param>
        /// <returns>
        /// A task that represents an asynchronous operation containing the result of setting the main photo.
        /// </returns>
        /// <exception cref="UnauthorizedException">Thrown out when the user is not authorized.</exception>
        /// <exception cref="BadRequestException">Thrown if the photo is already set as the main one or cannot be set.</exception>
        public async Task SetMainPhoto(long userId, long id)
        {
            if (userId != long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                throw new UnauthorizedException("You are not authorized.");

            var userFromDb = await _userManager.GetUserByIdAsync(userId);

            if (!userFromDb!.Photos.Any(p => p.Id == id))
                throw new UnauthorizedException("You are not authorized.");

            var photoFromDb = await _photoRepository.GetByIdAsync(id);

            if (photoFromDb!.IsMain)
                throw new BadRequestException("This is already the main photo.");

            var currentMainPhoto = await _repository.GetMainPhotoForUser(userId);
            currentMainPhoto!.IsMain = false;

            photoFromDb.IsMain = true;

            if (await _photoRepository.SaveAll() is not true)
                throw new BadRequestException("Could not set photo to main.");
        }

        /// <summary>
        /// Deletes the photo, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="userId">The user identifier to get for.</param>
        /// <param name="id">The photo identifier to get for.</param>
        /// <returns>
        /// A task that represents an asynchronous operation containing the result of deleting the user photo.
        /// </returns>
        /// <exception cref="UnauthorizedException">Thrown when the user is not authorized.</exception>
        /// <exception cref="BadRequestException">Thrown if the photo is the main one and cannot be deleted.</exception>
        public async Task DeletePhoto(long userId, long id)
        {
            if (userId != long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                throw new UnauthorizedException("You are not authorized.");

            var userFromDb = await _userManager.GetUserByIdAsync(userId);

            if (!userFromDb!.Photos.Any(p => p.Id == id))
                throw new UnauthorizedException("You are not authorized.");

            var photoFromDb = await _photoRepository.GetByIdAsync(id);

            if (photoFromDb!.IsMain)
                throw new BadRequestException("You cannot delete your main photo");

            if (photoFromDb.PublicId is not null)
            {
                var deleteParams = new DeletionParams(photoFromDb.PublicId);

                var deletionResult = _cloudinary.Destroy(deleteParams);

                if (deletionResult.Result == "ok") _photoRepository.Delete(photoFromDb);
            }

            if (photoFromDb.PublicId is null)
            {
                _photoRepository.Delete(photoFromDb);
            }

            if (await _photoRepository.SaveAll() is not true)
                throw new BadRequestException("Failed to delete the photo.");
        }
    }
}

