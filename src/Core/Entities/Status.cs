using System;
namespace Core.Entities
{
    /// <summary>
    /// Represents the user status
    /// </summary>
	public class Status : BaseEntity
    {
        /// <summary>
        /// Gets or sets name
        /// </summary>
        public string Name { get; set; } = String.Empty!;
    }
}

