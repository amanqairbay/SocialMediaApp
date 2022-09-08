using System;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IPhotoRepository
    {
        Task<Photo?> GetMainPhotoForUser(long userId);
    }
}

