using System;
using Core.Entities;

namespace Core.Specifications
{
    public class MessageForThreadSpecification : BaseSpecification<Message>
    {
        public MessageForThreadSpecification(long userId, long recipientId)
            : base(x => (x.RecipientId == userId && x.SenderId == recipientId) ||
                   (x.RecipientId == recipientId && x.SenderId == userId))
        {
            AddInclude(x => x.Sender);
            AddInclude(x => x.Sender.Photos);
            AddInclude(x => x.Recipient);
            AddInclude(x => x.Recipient.Photos);
            AddOrderByDescending(x => x.MessageSent);
        }
    }
}

