using System;
namespace Core.DTOs.User
{
    public class UserForUpdateDto
    {
        public string Email { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
		public string Surname { get; set; } = String.Empty;
        public DateTime DateOfBirth { get; set; } = default!;
        public string Interests { get; set; } = String.Empty;
        public long GenderId { get; set; }
        public long StatusId { get; set; }
        public long CityId { get; set; }
        public long RegionId { get; set; }
    }
}

