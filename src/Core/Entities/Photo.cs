using System;
namespace Core.Entities
{
    /// <summary>
    /// Represents the app users photo
    /// </summary>
    public class Photo : BaseEntity
    {
        /// <summary>
        /// Gets or sets the url
        /// </summary>
        public string Url { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the date added
        /// </summary>
        public DateTime? DateAdded { get; set; }

        /// <summary>
        /// Gets or sets the main photo
        /// </summary>
        public bool IsMain { get; set; } = default!;

        /// <summary>
        /// Gets or sets the public identifier
        /// </summary>
		public string PublicId { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the app user 
        /// </summary>
        public AppUser? AppUser { get; set; }

        /// <summary>
        /// Gets or sets the app user identifier
        /// </summary>
        public long? AppUserId { get; set; }
    }
}

