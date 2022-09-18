using System;
using Core.Entities;

namespace Core.Specifications
{
    public class LikeSpecification : BaseSpecification<Like>
    {
        public LikeSpecification(long userId, long recipientId) : base(x =>
            (userId == x.LikerId && recipientId == x.LikeeId))
        {
        }
    }
}

