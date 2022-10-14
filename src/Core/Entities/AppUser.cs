using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    /// <summary>
    /// Represents the app user
    /// </summary>
    public class AppUser : IdentityUser<long>
    {
        /// <summary>
        /// Gets or sets the name for this user.
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the surname for this user.
        /// </summary>
		public string Surname { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the date of birth for this user.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the creation date for this user.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the last active time for this user.
        /// </summary>
        public DateTime LastActive { get; set; }

        /// <summary>
        /// Gets or sets interests.
        /// </summary>
        public string Interests { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the gender for this user.
        /// </summary>
        public Gender? Gender { get; set; }

        // <summary>
        /// Gets or sets the gender identifier for this user.
        /// </summary>
        public long? GenderId { get; set; }

        /// <summary>
        /// Gets or sets the status for this user.
        /// </summary>
        public Status? Status { get; set; }

        /// <summary>
        /// Gets or sets the status identifier for this user.
        /// </summary>
        public long? StatusId { get; set; }

        /// <summary>
        /// Gets or sets the city for this user.
        /// </summary>
        public City? City { get; set; }

        /// <summary>
        /// Gets or sets the city identifier for this user.
        /// </summary>
        public long? CityId { get; set; }

        /// <summary>
        /// Gets or sets the region for this user.
        /// </summary>
        public Region? Region { get; set; }

        /// <summary>
        /// Gets or sets the region identifier for this user.
        /// </summary>
        public long? RegionId { get; set; }

        /// <summary>
        /// Gets or sets photos for this user.
        /// </summary>
        public ICollection<Photo> Photos { get; set; } = default!;

        /// <summary>
        /// Gets or sets likers for this user.
        /// </summary>
        public ICollection<Like> Likers { get; set; } = default!;

        /// <summary>
        /// Gets or sets likees for this user.
        /// </summary>
        public ICollection<Like> Likees { get; set; } = default!;

        /// <summary>
        /// Gets or sets sent messages for this user.
        /// </summary>
        public ICollection<Message> MessagesSent { get; set; } = default!;

        /// <summary>
        /// Gets or sets received messages for this user.
        /// </summary>
        public ICollection<Message> MessagesReceived { get; set; } = default!;
    }
}

