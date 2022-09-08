using System;
namespace Core.Entities
{
    /// <summary>
    /// Represents the city
    /// </summary>
	public class City : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the region
        /// </summary>
        public Region Region { get; set; } = default!;

        /// <summary>
        /// Gets or sets the region identifier
        /// </summary>
        public long RegionId { get; set; }
    }
}

