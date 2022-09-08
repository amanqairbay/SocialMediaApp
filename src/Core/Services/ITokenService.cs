using System;
using Core.Entities;

namespace Core.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}

