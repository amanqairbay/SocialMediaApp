using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.User
{
    public class UserForRegisterDto
    {
        [Required]
        public string Name { get; set; } = String.Empty;

        [Required]
        public string Surname { get; set; } = String.Empty;

        public long? GenderId { get; set; }
        public DateTime DateOfBirth { get; set; } = new DateTime(1990, 08, 21);
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
            ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non alphanumeric and at least 6 characters")]
        public string Password { get; set; } = String.Empty;
    }
}

