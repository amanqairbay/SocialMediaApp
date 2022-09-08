using System;
using AutoMapper;
using Core.DTOs.User;
using Core.Entities;
using Core.Errors;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.API.Extensions;

namespace Web.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);

            return new UserDto
            {
                Email = user!.Email,
                Token = _tokenService.CreateToken(user),
                Name = user.Name,
                Surname = user.Surname,
                DateOfBirth = user.DateOfBirth,
                Created = user.Created,
                LastActive = user.LastActive,
                Interests = user.Interests,
                StatusId = user.StatusId,
                GenderId = user.GenderId,
                CityId = user.CityId,
                RegionId = user.RegionId
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserToLoginDto loginDto)
        {
            var user = await _userManager.GetUserByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Name = user.Name,
                Surname = user.Surname,
                DateOfBirth = user.DateOfBirth,
                Created = user.Created,
                LastActive = user.LastActive,
                Interests = user.Interests,
                GenderId = user.GenderId,
                Gender = user.Gender!.Name,
                StatusId = user.StatusId,
                Status = user.Status!.Name,
                CityId = user.CityId,
                City = user.City!.Name,
                RegionId = user.RegionId,
                Region = user.Region!.Name,
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain)!.Url
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserToRegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = new[] { "Email address is in use" }
                });
            }

            var user = new AppUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                GenderId = 1,
                StatusId = 1,
                DateOfBirth = DateTime.Now,
                Created = DateTime.Now,
                LastActive = DateTime.Now,
                Interests = "Interests text",
                CityId = 1,
                RegionId = 1
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Name = user.Name,
                Surname = user.Surname,
                GenderId = user.GenderId,
                StatusId = user.StatusId,
                DateOfBirth = user.DateOfBirth,
                Created = user.Created,
                LastActive = user.LastActive,
                Interests = user.Interests,
                CityId = user.CityId,
                RegionId = user.RegionId
            };
        }
    }
}

