using System;
namespace Core.Entities
{
    /// <summary>
    /// Represents the user gender 
    /// </summary>
	public class Gender : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; } = String.Empty!;
    }
}

