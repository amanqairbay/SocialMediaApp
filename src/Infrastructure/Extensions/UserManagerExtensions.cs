using System;
using Core.Entities;
using Core.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Core.Specifications;
using Infrastructure.Data;

namespace Infrastructure.Extensions
{
    /// <summary>
    /// An extension class for providing user manager extensions.
    /// /// </summary>
    public static class UserManagerExtensions
    {
        /// <summary>
        /// Finds and returns a user.
        /// </summary>
        /// <param name="input">Input object.</param>
        /// <param name="user">The principal which contains the user email claim.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a user.
        /// </returns>
        public static async Task<AppUser?> FindByUserByClaimsPrincipleWithPhotosAsync(this UserManager<AppUser> input,
            ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await input.Users
                .Include(x => x.Gender)
                .Include(x => x.Status)
                .Include(x => x.Region)
                .Include(x => x.City)
                .Include(x => x.Photos)
                .SingleOrDefaultAsync(x => x.Email == email);
        }

        /// <summary>
        /// Finds and returns a user
        /// </summary>
        /// <param name="input">Input object.</param>
        /// <param name="user">The principal which contains the user email claim.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a user.
        /// </returns>
        public static async Task<AppUser?> FindByEmailFromClaimsPrinciple(this UserManager<AppUser> input,
            ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await input.Users
                .Include(x => x.Gender)
                .Include(x => x.Status)
                .Include(x => x.Region)
                .Include(x => x.City)
                .Include(x => x.Photos)
                .SingleOrDefaultAsync(x => x.Email == email);
        }

        /// <summary>
        /// Gets and returns a list of users
        /// </summary>
        /// <param name="input">Input object.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the users.
        /// </returns>
        public static async Task<IEnumerable<AppUser>> GetAllUsersAsync(this UserManager<AppUser> input)
        {
            var users = await input.Users
                .Include(x => x.Gender)
                .Include(x => x.Status)
                .Include(x => x.Region)
                .Include(x => x.City)
                .Include(x => x.Photos)
                .ToListAsync();

            return users;
        }

        /// <summary>
        /// Gets and returns a user, if any, who has the specified <paramref name="userId" />.
        /// </summary>
        /// <param name="input">Input object.</param>
        /// <param name="userId">The user identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the user matching the specified <paramref name="userId" /> if it exists.
        /// </returns>
        public static async Task<AppUser?> GetUserByIdAsync(this UserManager<AppUser> input, long userId)
        {
            var user = await input.Users
                .Include(x => x.Gender)
                .Include(x => x.Status)
                .Include(x => x.Region)
                .Include(x => x.City)
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.Id == userId);

            return user;
        }

        /// <summary>
        /// Gets and returns a user, if any, who has the specified <paramref name="email" />.
        /// </summary>
        /// <param name="input">Input object.</param>
        /// <param name="email">The user email to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the user matching the specified <paramref name="email" /> if it exists.
        /// </returns>
        public static async Task<AppUser?> GetUserByEmailAsync(this UserManager<AppUser> input, string email)
        {
            var user = await input.Users
                .Include(x => x.Gender)
                .Include(x => x.Status)
                .Include(x => x.Region)
                .Include(x => x.City)
                .Include(x => x.Photos)
                .SingleOrDefaultAsync(x => x.Email == email);

            return user;
        }

        /// <summary>
        /// Gets and returns a list of users by parameters
        /// </summary>
        /// <param name="input">Input object.</param>
        /// <param name="userParameters">The user parameters to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of users.
        /// </returns>
        public static async Task<PagedList<AppUser>> GetPagedListUsersAsync(this UserManager<AppUser> input, UserParameters userParameters)
        {
            var specification = new UserSpecification(userParameters);
            var countSpecification = new UserWithFiltersForCountSpecification(userParameters);
            var totalItems = await ApplySpecification(input, countSpecification).CountAsync();
            var users = await ApplySpecification(input, specification).ToListAsync();

            if (userParameters.MinAge != 18 || userParameters.MaxAge != 99)
            {
                var minDob = DateTime.Today.AddYears(-userParameters.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userParameters.MinAge);

                users = users.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob).ToList();
            }

            if (userParameters.Likers || userParameters.Likees)
            {
                var userLikers = await GetUserLikes(input, (long)userParameters.UserId!, userParameters.Likers);
                users = users.Where(u => userLikers.Contains(u.Id)).ToList();
            }

            return new PagedList<AppUser>(users, totalItems, userParameters.PageIndex, userParameters.PageSize);
        }

        /// <summary>
        /// Gets and returns a user likes
        /// </summary>
        /// <param name="input">Input object.</param>
        /// <param name="userId">The user identifier to get for.</param>
        /// <param name="likers">The user's likers.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of likes.
        /// </returns>
        private static async Task<IEnumerable<long>> GetUserLikes(this UserManager<AppUser> input, long userId, bool likers)
        {
            var user = await input.Users
                .Include(x => x.Likers)
                .Include(x => x.Likees)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (likers)
            {
                return user!.Likers.Where(u => u.LikeeId == userId).Select(i => i.LikerId);
            }

            return user!.Likees.Where(u => u.LikerId == userId).Select(i => i.LikeeId);
        }

        private static IQueryable<AppUser> ApplySpecification(UserManager<AppUser> input, ISpecification<AppUser> specification) =>
            UserSpecificationEvaluator<AppUser>.GetQuery(input.Users.AsQueryable(), specification);
    }
}

