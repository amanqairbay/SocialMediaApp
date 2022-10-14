using System;
namespace Core.RequestFeatures
{
    /// <summary>
    /// Represents the message parameters.
    /// </summary>
    public class MessageParameters : RequestParameters
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the message container.
        /// </summary>
        public string MessageContainer { get; set; } = "Unread";
    }
}

