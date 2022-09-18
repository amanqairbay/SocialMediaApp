using System;
namespace Core.RequestFeatures
{
    /// <summary>
    /// User parameters
    /// </summary>
    public class UserParameters : RequestParameters
    {
        /// <summary>
        /// Gets or sets user identifier
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Gets or sets gender
        /// </summary>
        public long? GenderId { get; set; }

        /// <summary>
        /// Gets or sets min age
        /// </summary>
        public int MinAge { get; set; } = 18;

        /// <summary>
        /// Gets or sets max age
        /// </summary>
        public int MaxAge { get; set; } = 99;

        /// <summary>
        /// Gets or sets sort
        /// </summary>
        public string Sort { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets search
        /// </summary>
        public string Search { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets likees
        /// </summary>
        public bool Likees { get; set; } = false;

        /// <summary>
        /// Gets or sets likers
        /// </summary>
        public bool Likers { get; set; } = false;
    }
}

