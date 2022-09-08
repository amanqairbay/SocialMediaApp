using System;
namespace Core.DTOs.User
{
    public class UserToLoginDto
    {
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
}

