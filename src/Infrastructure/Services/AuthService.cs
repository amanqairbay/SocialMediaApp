using AutoMapper;
using Core.DTOs.User;
using Core.Entities;
using Core.Exceptions;
using Core.Services;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    /// <summary>
    /// Represents the authentication servise.
    /// </summary>
    public class AuthService : IAuthService
    {
        #region Fields
        private readonly IHttpContextAccessor _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion Fields

        #region Constructor
        public AuthService(
            IHttpContextAccessor context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #endregion Constructor

        /// <summary>
        /// Gets and returns the current user.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the user.
        /// </returns>
        public async Task<UserDto> GetCurrentUserAsync()
        {
            var userFromDb = await _userManager.FindByEmailFromClaimsPrinciple(_context.HttpContext.User);
            
            var userDto = _mapper.Map<AppUser, UserDto>(userFromDb!);

            userDto.Token = _tokenService.CreateToken(userFromDb!);

            return userDto;
        }

        /// <summary>
        /// Checks if the mail exists.
        /// </summary>
        /// <param name="email">The user email to check for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing a boolean result.
        /// </returns>
        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        /// <summary>
        /// User log in.
        /// </summary>
        /// <param name="loginDto">The user data to login for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the user.
        /// </returns>
        public async Task<UserDto> Login(UserToLoginDto loginDto)
        {
            var user = await _userManager.GetUserByEmailAsync(loginDto.Email);

            if (user == null) throw new UnauthorizedException("You are not authorized");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) throw new UnauthorizedException("You are not authorized");

            var userFromDb = await _userManager.GetUserByEmailAsync(user.Email);

            var userDto = _mapper.Map<AppUser, UserDto>(userFromDb!);

            userDto.Token = _tokenService.CreateToken(user);

            return userDto;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerDto">The user dat to register for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the user.
        /// </returns>
        public async Task<UserDto> Register(UserForRegisterDto registerDto)
        {
            var checkEmail = await CheckEmailExistsAsync(registerDto.Email);

            if (checkEmail) throw new BadRequestException("Email address is in use.");

            var user = new AppUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                GenderId = registerDto.GenderId,
                DateOfBirth = registerDto.DateOfBirth,
                Created = DateTime.Now,
                LastActive = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) throw new BadRequestException("Invalid syntax for this request was provided.");

            var userFromDb = await _userManager.GetUserByEmailAsync(user.Email);

            var userDto = _mapper.Map<AppUser, UserDto>(userFromDb!);

            userDto.Token = _tokenService.CreateToken(user);

            return userDto;
        }
    }
}

