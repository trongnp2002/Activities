using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Security;

public class IsHostRequirement : IAuthorizationRequirement
{

}

public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
{
    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public IsHostRequirementHandler(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _dataContext = dataContext;

    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) return;

        var activityId = Guid.Parse(_httpContextAccessor.HttpContext?
        .Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value?.ToString());

        var attendee = await _dataContext.ActivityAttendees.AsNoTracking()
        .SingleOrDefaultAsync(x => x.AppUserId == userId && x.ActivityId == activityId);
        if (attendee is null) return;
        if (attendee.IsHost) context.Succeed(requirement);
        return;

    }
}
