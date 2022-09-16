using System;
using Core.Entities;
using Core.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Core.Specifications;
using Infrastructure.Data;

namespace Web.API.Extensions
{
    public static class UserManagerExtensions
    {
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

        public static async Task<AppUser?> GetUserByIdAsync(this UserManager<AppUser> input, long id)
        {
            var user = await input.Users
                .Include(x => x.Gender)
                .Include(x => x.Status)
                .Include(x => x.Region)
                .Include(x => x.City)
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

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

            return new PagedList<AppUser>(users, totalItems, userParameters.PageIndex, userParameters.PageSize);
        }
        
        private static IQueryable<AppUser> ApplySpecification(UserManager<AppUser> input, ISpecification<AppUser> specification) =>
            UserSpecificationEvaluator<AppUser>.GetQuery(input.Users.AsQueryable(), specification);
    }
}

