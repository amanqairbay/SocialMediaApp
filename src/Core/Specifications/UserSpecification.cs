using System;
using Core.Entities;
using Core.RequestFeatures;

namespace Core.Specifications
{
    public class UserSpecification : BaseSpecification<AppUser>
    {
        public UserSpecification(UserParameters userParameters)
            : base(x => (string.IsNullOrEmpty(userParameters.Search) || x.Name.ToLower().Contains(userParameters.Search)))      
        {
            AddInclude(x => x.Photos);
            AddOrderBy(x => x.Name);
            ApplyPaging(userParameters.PageSize * (userParameters.PageIndex - 1), userParameters.PageSize);

            if (!string.IsNullOrEmpty(userParameters.Sort))
            {
                switch (userParameters.Sort)
                {
                    case "surname":
                        AddOrderBy(x => x.Surname);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
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

