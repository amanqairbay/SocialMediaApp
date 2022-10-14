using System;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    /// <summary>
    /// Represents the message
    /// </summary>
    public class Message : BaseEntity
    {
        /// <summary>
        /// Gets or sets the sender identifier
        /// </summary>
        public long SenderId { get; set; }

        /// <summary>
        /// Gets or sets the sender
        /// </summary>
        public AppUser Sender { get; set; } = default!;

        /// <summary>
        /// Gets or sets the recipient identifier
        /// </summary>
        public long RecipientId { get; set; }

        /// <summary>
        /// Gets or sets the recipient
        /// </summary>
        public AppUser Recipient { get; set; } = default!;

        /// <summary>
        /// Gets or sets message content
        /// </summary>
        public string Content { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the is read message
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Gets or sets the date read
        /// </summary>
        public DateTime? DateRead { get; set; }

        /// <summary>
        /// Gets or sets the sent message
        /// </summary>
        public DateTime MessageSent { get; set; }

        /// <summary>
        /// Gets or sets the deleted sender
        /// </summary>
        public bool SenderDeleted { get; set; }

        /// <summary>
        /// Gets or sets the deleted recipient
        /// </summary>
        public bool RecipientDeleted { get; set; }
    }
}

