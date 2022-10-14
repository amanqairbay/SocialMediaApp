using System;
namespace Core.DTOs.Message
{
    public class MessageToReturnDto
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public string SenderName { get; set; } = String.Empty!;
        public string SenderPhotoUrl { get; set; } = String.Empty!;
        public long RecipientId { get; set; }
        public string RecipientName { get; set; } = String.Empty!;
        public string RecipientPhotoUrl { get; set; } = String.Empty!;
        public string Content { get; set; } = String.Empty!;
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
    }
}

