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
        /// Gets or sets sort
        /// </summary>
        public string Sort { get; set; } = "name";

        /// <summary>
        /// Gets or sets search
        /// </summary>
        public string Search { get; set; } = String.Empty;
    }
}

