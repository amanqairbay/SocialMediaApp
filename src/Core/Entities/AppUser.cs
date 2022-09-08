using System;
using Microsoft.AspNetCore.Identity;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Core.Entities
{
    /// <summary>
    /// Represents the app user
    /// </summary>
    public class AppUser : IdentityUser<long>
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the surname
        /// </summary>
		public string Surname { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the date of birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the creation date
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the last active time
        /// </summary>
        public DateTime LastActive { get; set; }

        /// <summary>
        /// Gets or sets user interests
        /// </summary>
        public string Interests { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the gender
        /// </summary>
        public Gender? Gender { get; set; }

        // <summary>
        /// Gets or sets the gender identifier
        /// </summary>
        public long? GenderId { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public Status? Status { get; set; }

        /// <summary>
        /// Gets or sets the status identifier
        /// </summary>
        public long? StatusId { get; set; }

        /// <summary>
        /// Gets or sets the city
        /// </summary>
        public City? City { get; set; }

        /// <summary>
        /// Gets or sets the city identifier
        /// </summary>
        public long? CityId { get; set; }

        /// <summary>
        /// Gets or sets the region
        /// </summary>
        public Region? Region { get; set; }

        /// <summary>
        /// Gets or sets the region identifier
        /// </summary>
        public long? RegionId { get; set; }

        /// <summary>
        /// Gets or sets user photos
        /// </summary>
        public ICollection<Photo> Photos { get; set; } = default!;
    }
}

