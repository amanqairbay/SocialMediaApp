using System;
namespace Infrastructure.Helpers
{
    /// <summary>
    /// Represents the cloudinary settings.
    /// </summary>
	public class CloudinarySettings
    {
        /// <summary>
        /// Gets or sets the cloud name.
        /// </summary>
        public string CloudName { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the api key.
        /// </summary>
		public string ApiKey { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the api secret.
        /// </summary>
        public string ApiSecret { get; set; } = String.Empty;
    }
}

