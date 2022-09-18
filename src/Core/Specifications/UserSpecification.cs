using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Entities;
using Core.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Core.Specifications
{
    public class UserSpecification : BaseSpecification<AppUser>
    {
        public UserSpecification(UserParameters userParameters)
            : base(x =>
                (string.IsNullOrEmpty(userParameters.Search) || x.Name.ToLower().Contains(userParameters.Search)) &&
                (!userParameters.UserId.HasValue || userParameters.UserId != x.Id) &&
                (!userParameters.GenderId.HasValue || userParameters.GenderId == x.GenderId))
        {
            AddInclude(x => x.Photos);
            AddInclude(x => x.Likers);
            AddInclude(x => x.Likees);

            AddOrderBy(x => x.Name);
            ApplyPaging(userParameters.PageSize * (userParameters.PageIndex - 1), userParameters.PageSize);

            if (!string.IsNullOrEmpty(userParameters.Sort))
            {
                switch (userParameters.Sort)
                {
                    case "created":
                        AddOrderByDescending(x => x.Created);
                        break;
                    default:
                        AddOrderByDescending(x => x.LastActive);
                        break;
                }
            }

            
        }

        public UserSpecification(long userId) : base(x => x.Id == userId)
        {
            AddInclude(x => x.Photos);
        }
    }
}

