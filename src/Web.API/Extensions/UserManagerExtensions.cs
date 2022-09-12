using System;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
    }
}

