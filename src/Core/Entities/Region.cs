using System;
namespace Core.Entities
{
    /// <summary>
    /// Represents the region
    /// </summary>
	public class Region : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the city collection
        /// </summary>
        public ICollection<City> Cities { get; set; } = default!;
    }
}

