using System;
namespace Core.Entities
{
    /// <summary>
    /// Represents the like class
    /// </summary>
    public class Like
    {
        /// <summary>
        /// Gets or sets the liker identifier
        /// </summary>
        public long LikerId { get; set; }

        /// <summary>
        /// Gets or sets the like identifier
        /// </summary>
        public long LikeeId { get; set; }

        /// <summary>
        /// Gets or sets the liker
        /// </summary>
        public AppUser? Liker { get; set; }

        /// <summary>
        /// Gets or sets the likee
        /// </summary>
        public AppUser? Likee { get; set; }
    }
}

