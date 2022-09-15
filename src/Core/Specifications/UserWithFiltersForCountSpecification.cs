using System;
using Core.Entities;
using Core.RequestFeatures;

namespace Core.Specifications
{
    public class UserWithFiltersForCountSpecification : BaseSpecification<AppUser>
    {
        public UserWithFiltersForCountSpecification(UserParameters userParameters)
            : base(x => (string.IsNullOrEmpty(userParameters.Search) || x.Name.ToLower().Contains(userParameters.Search)))
        {}
    }
}

