using System;
using System.Security.Claims;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.API.Extensions;

namespace Web.API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var userId = long.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var repository = resultContext.HttpContext.RequestServices.GetService<UserManager<AppUser>>();
            var user = await repository!.GetUserByIdAsync(userId);
            user!.LastActive = DateTime.Now;
            await repository!.UpdateAsync(user);
        }
    }
}

