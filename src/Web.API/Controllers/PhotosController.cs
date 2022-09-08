using System;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.DTOs.Photo;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Xml.Linq;
using Web.API.Helpers;
using Web.API.Extensions;

namespace Web.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    public class PhotosController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Photo> _photoRepository;
        private readonly IPhotoRepository _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(
            IMapper mapper,
            IRepository<Photo> photoRepository,
            IPhotoRepository repository,
            UserManager<AppUser> userManager,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _mapper = mapper;
            _photoRepository = photoRepository;
            _repository = repository;
            _userManager = userManager;
            _cloudinaryConfig = cloudinaryConfig;

            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        [HttpGet("/{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(long id)
        {
            var photoFromDb = await _photoRepository.GetByIdAsync(id);
            var photoDto = _mapper.Map<PhotoForReturnDto>(photoFromDb);

            return Ok(photoDto);
        }

        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> AddPhotoForUser(long userId, [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            if (userId != long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                return Unauthorized();

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

            if (!userFromDb!.Photos.Any(u => u.IsMain)) { photo.IsMain = true; }

            userFromDb.Photos.Add(photo);

            if (await _photoRepository.SaveAll())
            {
                var photoForReturnDto = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoForReturnDto);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(long userId, long id)
        {
            if (userId != long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                return Unauthorized();

            var userFromDb = await _userManager.GetUserByIdAsync(userId);

            if (!userFromDb!.Photos.Any(p => p.Id == id)) return Unauthorized();

            var photoFromDb = await _photoRepository.GetByIdAsync(id);

            if (photoFromDb!.IsMain) return BadRequest("This is already the main photo");

            var currentMainPhoto = await _repository.GetMainPhotoForUser(userId);
            currentMainPhoto!.IsMain = false;

            photoFromDb.IsMain = true;

            if (await _photoRepository.SaveAll()) return NoContent();

            return BadRequest("Could not set photo to main");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(long userId, long id)
        {
            if (userId != long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                return Unauthorized();

            var userFromDb = await _userManager.GetUserByIdAsync(userId);

            if (!userFromDb!.Photos.Any(p => p.Id == id)) return Unauthorized();

            var photoFromDb = await _photoRepository.GetByIdAsync(id);

            if (photoFromDb!.IsMain) return BadRequest("You cannot delete your main photo");

            if (photoFromDb.PublicId != null)
            {
                var deleteParams = new DeletionParams(photoFromDb.PublicId);

                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok") _photoRepository.Delete(photoFromDb);
            }

            if (photoFromDb.PublicId == null)
            {
                _photoRepository.Delete(photoFromDb);
            }

            if (await _photoRepository.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Failed to delete the photo");
        }
    }
}

