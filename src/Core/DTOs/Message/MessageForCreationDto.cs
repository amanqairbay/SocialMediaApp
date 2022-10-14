using System;
namespace Core.DTOs.Message
{
    public class MessageForCreationDto
    {
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public DateTime MessageSent { get; set; }
        public string Content { get; set; } = String.Empty;

        public MessageForCreationDto()
        {
            MessageSent = DateTime.Now;
        }
    }
}

