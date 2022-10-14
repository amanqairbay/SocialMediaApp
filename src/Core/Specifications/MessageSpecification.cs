using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.RequestFeatures;

namespace Core.Specifications
{
    public class MessageSpecification : BaseSpecification<Message>
    {
        public MessageSpecification(MessageParameters messageParameters)
            : base()
        {
            AddInclude(x => x.Sender);
            AddInclude(x => x.Sender.Photos);
            AddInclude(x => x.Recipient);
            AddInclude(x => x.Recipient.Photos);
            AddOrderByDescending(x => x.MessageSent);

            if (!string.IsNullOrEmpty(messageParameters.MessageContainer))
            {
                switch (messageParameters.MessageContainer)
                {
                    case "Inbox":
                        Criteria = x => x.RecipientId == messageParameters.UserId;
                        break;
                    case "Outbox":
                        Criteria = x => x.SenderId == messageParameters.UserId;
                        break;
                    default:
                        Criteria = x => x.RecipientId == messageParameters.UserId && x.IsRead == false;
                        break;
                }
            }
        }
    }
}

