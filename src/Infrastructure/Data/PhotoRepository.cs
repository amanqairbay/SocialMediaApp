using System;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly ApplicationDbContext _context;

        public PhotoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Photo?> GetMainPhotoForUser(long userId)
        {
            return await _context.Photos.Where(u => u.AppUserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }
    }
}

