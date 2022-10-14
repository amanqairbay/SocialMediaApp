using System.Security.Claims;
using AutoMapper;
using Core.DTOs.User;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.RequestFeatures;
using Core.Services;
using Core.Specifications;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    /// <summary>
    /// Represents user service class.
    /// </summary>
    public class UserService : IUserService
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Like> _likeRepository;
        private readonly IMapper _mapper;
        #endregion Fields

        #region Constructor
        public UserService(
            IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager,
            IRepository<Like> likeRepository,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _likeRepository = likeRepository;
            _mapper = mapper;
        }
        #endregion Constructor

        #region Get methods
        /// <summary>
        /// Gets and returns a list of users by parameters.
        /// </summary>
        /// <param name="userParams">The user params to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of users.
        /// </returns>
        public async Task<(IEnumerable<UserForListDto> users, MetaData metaData)> GetPagedListUsersAsync(UserParameters userParams)
        {
            var currentUserId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            userParams.UserId = currentUserId;

            if (userParams.GenderId != null)
            {
                var userFromDb = await _userManager.GetUserByIdAsync((long)userParams.UserId!);
                userParams.GenderId = userParams.GenderId == 1 ? 2 : 1;
            }

            var pagedList = await _userManager.GetPagedListUsersAsync(userParams);

            var usersFromDb = pagedList.Items;

            var usersDto = _mapper.Map<IEnumerable<UserForListDto>>(usersFromDb);

            return (users: usersDto, metaData: pagedList.MetaData);
        }

        /// <summary>
        /// Gets and returns a user, if any, who has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The user identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the user matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        public async Task<UserForDetailedDto> GetUserByIdAsync(long id)
        {
            var userFromDb = await GetUserAndCheckIfItExists(id);

            var userForDetailedDto = _mapper.Map<AppUser, UserForDetailedDto>(userFromDb);

            return userForDetailedDto;
        }

        /// <summary>
        /// Gets and returns a user, if any, who has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The user identifier to get for.</param>
        /// <param name="user">Optional parameter.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the user matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        /// <exception cref="NotFoundException">Thrown if the user with doesn't exist in the database.</exception>
        private async Task<AppUser> GetUserAndCheckIfItExists(long id, string? user = null)
        {
            if (user is null) { user = "user"; }

            var userFromDb = await _userManager.GetUserByIdAsync(id);

            if (userFromDb is null)
                throw new NotFoundException($"The {user} with id: {id} doesn't exist in the database.");

            return userFromDb;
        }
        #endregion Get methods

        /// <summary>
        /// Checks a user, if any, who has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The user identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation checking whether the user exists.
        /// </returns>
        /// <exception cref="NotFoundException">Thrown if the user doesn't exist.</exception>
        public async Task CheckIfUserExists(long id)
        {
            var userFromDb = await _userManager.GetUserByIdAsync(id);

            if (userFromDb is null)
                throw new NotFoundException($"The user with id: {id} doesn't exist in the database.");
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="id">The user identifier to get for.</param>
        /// <param name="userForUpdateDto">The user to update for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the result of the user update.
        /// </returns>
        /// <exception cref="UnauthorizedException">Thrown if the current user is not logged in.</exception>
        /// <exception cref="BadRequestException">Thrown if the user update could not be saved.</exception>
        public async Task UpdateUser(long id, UserForUpdateDto userForUpdateDto)
        {
            if (id != long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                throw new UnauthorizedException("You are not authorized.");

            var userFromDb = await GetUserAndCheckIfItExists(id);

            var userDto = _mapper.Map(userForUpdateDto, userFromDb);

            var result = await _userManager.UpdateAsync(userDto);

            if (result.Succeeded is not true)
                throw new BadRequestException($"Updating user {id} failed on save.");
        }

        /// <summary>
        /// Likes the user.
        /// </summary>
        /// <param name="id">The user identifier to get for.</param>
        /// <param name="recipientId">The recipient identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the result of the user's like.
        /// </returns>
        /// <exception cref="UnauthorizedException">Thrown if the current user is not logged in.</exception>
        /// <exception cref="BadRequestException">Thrown if the user already like or failed to like.</exception>
        public async Task LikeUser(long id, long recipientId)
        {
            if (id != long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                throw new UnauthorizedException("You are not authorized.");

            var specification = new LikeSpecification(id, recipientId);
            var like = await _likeRepository.GetEntityWithSpecification(specification);

            if (like is not null) throw new BadRequestException("You already like this user.");

            await CheckIfUserExists(recipientId);

            like = new Like { LikerId = id, LikeeId = recipientId };

            _likeRepository.Add(like);

            if(await _likeRepository.SaveAll() is not true)
                throw new BadRequestException("Failed to like user.");
        }
    }
}

